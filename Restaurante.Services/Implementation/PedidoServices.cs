using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurante.Const;
using Restaurante.DAO;
using Restaurante.Infraestructure;
using Restaurante.Models;
using Restaurante.Models.Enums;


namespace Restaurante.Services
{
    public class PedidoServices : IPedidoServices
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoServices _prodServices;

        public PedidoServices(IMapper mapper, IUserServices userServices, IUnitOfWork unitOfWork, IProductoServices prodServices)
        {
            _mapper = mapper;
            _userServices = userServices;
            _unitOfWork = unitOfWork;
            _prodServices = prodServices;
        }
        /// <summary>
        /// Puede alterar el pedido cuando se este buscando o cuando se este preparando y hay un recepcionista.
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        public async Task<ResultResponse> InteractuarPedidoActual(PedidoDTO dTO)
        {
            var result = new ResultResponse();
            int customStatusCode = 0;
            var newStatus = dTO.Estado;
            var pedidoData = dTO.Data != null ? _mapper.Map<Pedido>(dTO) : null;
            var userRole = _userServices.CurrentRol;
            var userId = _userServices.CurrentId;

            if (!string.IsNullOrEmpty(userRole) && userId.HasValue)
            {
                await _unitOfWork.BeginTransactionAsync();
                Pedido pedidoActual = null;
                bool success = true;

                if(string.IsNullOrEmpty(pedidoData.Id))
                    pedidoActual = await _unitOfWork.Pedido.PedidoActual(userId.Value);
                else
                    pedidoActual = await _unitOfWork.Pedido.WhereActive(x => x.Id == pedidoData.Id).FirstOrDefaultAsync();

                if(pedidoActual != null)
                {

                    (success, result.Message) = await ModificarEstadoPedido(pedidoActual, pedidoData, userRole);

                    #region Actualizar Productos Pedido
                    if (Roles.Administrador == userRole || success)
                    {
                        if(EstadoPedido.Buscando == pedidoActual.Estado)
                        {
                            var (opInserted, opUpdate, opDelete, opReactivate) = await ModificarArticulosPedidoEstadoBuscando(pedidoActual, pedidoData);
                            result.Message = string.IsNullOrEmpty(result.Message) ? $@"OPERATION ""{nameof(DetallePedido)},{opInserted},{opUpdate},{opDelete},{opReactivate}""" : $@"{result.Message}|OPERATION ""{nameof(DetallePedido)},{opInserted},{opUpdate},{opDelete},{opReactivate}""";
                        }
                        else
                        {
                            if (EstadoPedido.Preparando == pedidoActual.Estado && Roles.Recepcionista == userRole)
                            {
                                var (opInserted, opUpdate, opDelete, opReactivate) = await ModificarArticulosPedidoEstadoBuscando(pedidoActual, pedidoData);
                                if(opInserted > 0 || opReactivate > 0 || opUpdate > 0 || opDelete > 0)
                                {
                                    var nuevaInfoConsumo = new ConsumirProductoDTO();
                                    nuevaInfoConsumo.Data =
                                    pedidoActual.DiferenciaDetallePedido(pedidoData)
                                    .Detalles
                                    .Select(x => new ConsumirProductoDTO.ConsumirProductoDataDTO { Cantidad = x.Cantidad, Id = x.Id }).ToList();

                                    var nuevoConsumo = new ConsumirProductoDTO();
                                    nuevoConsumo.Data = nuevaInfoConsumo.Data.Where(x => x.Cantidad > 0).ToList();

                                    var nuevaRestauracion = new ConsumirProductoDTO();
                                    nuevaRestauracion.Data = nuevaInfoConsumo.Data.Where(x => x.Cantidad < 0).ToList();

                                    var restaurarITask = await _prodServices.RestaurarIngredientes(nuevaRestauracion);
                                    var consumirTask = await _prodServices.Consumir(nuevoConsumo);

                                    customStatusCode = restaurarITask.StatusCode >= 400 || consumirTask.StatusCode >= 400 ? 400 : 200;
                                    result.Message = string.Join("|",new string[] {restaurarITask.Message,consumirTask.Message});
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    success = false;
                    result.Message = $@"ENTITY_NOT_FOUND ""{nameof(Pedido)},{pedidoData.Id}""";
                }

                if (success)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    result.StatusCode = customStatusCode != 0 ? customStatusCode : 200;
                }
                else
                { 
                    await _unitOfWork.RollbackTransactionAsync();
                    result.StatusCode = 400;
                }

            }
            else
            {
                result.StatusCode = 401;
                result.Message = "NO_ROLE";
            }
            return result;
        }

        /// <summary>
        /// Cambia el estado del pedido segun el rol
        /// </summary>
        /// <param name="pedidoActual"></param>
        /// <param name="nuevaInformacion"></param>
        /// <param name="rolUsuario"></param>
        /// <returns>(Success|Informacion)</returns>
        public async Task<(bool,string)> ModificarEstadoPedido(Pedido pedidoActual, Pedido nuevaInformacion, string rolUsuario)
        {
            var success = true;
            string resultInfo = string.Empty;
            #region Cambiar Estado Pedido
            if (nuevaInformacion.Estado == EstadoPedido.Cancelado || rolUsuario == Roles.Administrador)
            {
                pedidoActual.Estado = nuevaInformacion.Estado;
                await _unitOfWork.Pedido.Update(pedidoActual);
            }
            else
            {
                if (Array.IndexOf(ParametroDeConfiguracion.EstadosPedidoOrdenado, nuevaInformacion.Estado) > Array.IndexOf(ParametroDeConfiguracion.EstadosPedidoOrdenado, pedidoActual.Estado))
                {
                    if (
                           (Roles.Recepcionista == rolUsuario && ParametroDeConfiguracion.EstadosPosiblesRecepcionista.Contains(nuevaInformacion.Estado))
                        || (Roles.Cocinero == rolUsuario && ParametroDeConfiguracion.EstadosPosiblesCocinero.Contains(nuevaInformacion.Estado))
                        || (Roles.Delivery == rolUsuario && ParametroDeConfiguracion.EstadosPosiblesDelivery.Contains(nuevaInformacion.Estado))
                        || (Roles.Cliente == rolUsuario && ParametroDeConfiguracion.EstadosPosiblesCliente.Contains(nuevaInformacion.Estado))
                    )
                    {
                        pedidoActual.Estado = nuevaInformacion.Estado;
                        await _unitOfWork.Pedido.Update(pedidoActual);
                    }
                    else
                    {
                        resultInfo = $@"ROL_NO_PERMISSION_CHANGE_STATUS ""{rolUsuario},{nuevaInformacion.Estado}""";
                        success = false;
                    }
                }
                else if (pedidoActual.Estado != EstadoPedido.Buscando)
                {
                    resultInfo = $@"ROL_NO_PERMISSION_CHANGE_STATUS ""{rolUsuario},{nuevaInformacion.Estado}""";
                    success = false;
                }
            }
            #endregion
            return (success,resultInfo);
        }

        /// <summary>
        /// Modifica la informacion actual del pedido cuando se encuentra en estado BUSCANDO
        /// </summary>
        /// <param name="pedidoActual"></param>
        /// <param name="nuevaInformacion"></param>
        /// <returns>ABM Count</returns>
        public async Task<(int,int,int,int)> ModificarArticulosPedidoEstadoBuscando(Pedido pedidoActual, Pedido nuevaInformacion)
        {
            var toDeactivate = pedidoActual.Detalles.Where(x => !nuevaInformacion.Detalles.Select(x => x.ProductoId).Contains(x.ProductoId));
            var toUpdate = pedidoActual.Detalles.Where(x =>
            {
                var a = nuevaInformacion.Detalles.FirstOrDefault(y => x.Id == y.Id);
                return a == null && !toDeactivate.Contains(x) || a != null && a.Cantidad != x.Cantidad;
            });
            var toInsert = nuevaInformacion.Detalles.Where(x => pedidoActual.Detalles.FirstOrDefault(y => y.ProductoId == x.ProductoId) == null).Select(x => { x.PedidoId = pedidoActual.Id; return x; });

            var opDelete = await _unitOfWork.DetallePedido.Delete(toDeactivate);
            var opUpdate = await _unitOfWork.DetallePedido.Update(toUpdate);
            var toInsertId = toInsert.Select(x => x.ProductoId);

            var toReactivate = await _unitOfWork.DetallePedido.WhereSoftDeleted(x => toInsertId.Contains(x.ProductoId)).ToListAsync();
            var opReactivate = await _unitOfWork.DetallePedido.Update(toReactivate.Select(x => { x.DeletedAt = null; x.Cantidad = toInsert.FirstOrDefault(y => y.Id == x.Id).Cantidad; return x; }));

            toInsert = toInsert.Where(x => toReactivate.FirstOrDefault(y => y.ProductoId == x.ProductoId) == null).ToList();
            var opInserted = await _unitOfWork.DetallePedido.Insert(toInsert);
            return (opInserted, opUpdate, opDelete, opReactivate);
        }
    }
}
