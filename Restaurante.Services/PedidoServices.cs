using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Restaurante.Const;
using Restaurante.DAO;
using Restaurante.Hubs;
using Restaurante.Infraestructure;
using Restaurante.Models;
using Restaurante.Models.Enums;
using Restaurante.Services.Extensions;


namespace Restaurante.Services
{
    public class PedidoServices : IPedidoServices
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoServices _prodServices;
        private readonly IHubContext<PedidoHub> _pedidoHub;
        private readonly IMessageServices _smsServices;

        public string SmsGroupId { get; set; }

        public PedidoServices(IMapper mapper, IUserServices userServices, IUnitOfWork unitOfWork, IProductoServices prodServices, IHubContext<PedidoHub> pedidoHub, IMessageServices smsServices)
        {
            _mapper = mapper;
            _userServices = userServices;
            _unitOfWork = unitOfWork;
            _prodServices = prodServices;
            _pedidoHub = pedidoHub;
            _smsServices = smsServices;
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
                    pedidoActual = await _unitOfWork.Pedido.Where(x => x.Id == pedidoData.Id).Include(x => x.Detalles).FirstOrDefaultAsync();

                if(pedidoActual != null)
                {
                    (success, result.Message) = await ModificarEstadoPedido(pedidoActual, pedidoData, userRole);

                    #region Actualizar Productos Pedido
                    if (Roles.Administrador == userRole || success)
                    {
                        if(EstadoPedido.Buscando == pedidoActual.Estado)
                        {
                            var (opInserted, opUpdate, opDelete, opReactivate) = await ModificarArticulosPedidoEstadoBuscando(pedidoActual, pedidoData);
                            result.Content = pedidoActual;
                            result.Message = string.IsNullOrEmpty(result.Message) ? $@"OPERATION ""{nameof(DetallePedido)},{opInserted},{opUpdate},{opDelete},{opReactivate}""" : $@"{result.Message}|OPERATION ""{nameof(DetallePedido)},{opInserted},{opUpdate},{opDelete},{opReactivate}""";
                            await _smsServices.SendOperation(result.Message);
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
                    await _smsServices.SendMessage(result.Message, result.StatusCode);
                }

                if (success)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    await _pedidoHub.Clients.Group(pedidoActual.Id).SendAsync(MethodsHub.UpdateOrder, _mapper.Map<PedidoDTO>(pedidoActual));
                    result.StatusCode = customStatusCode != 0 ? customStatusCode : StatusCodes.Status200OK;
                }
                else
                { 
                    await _unitOfWork.RollbackTransactionAsync();
                    if(!string.IsNullOrEmpty(pedidoActual.Id))
                        await _pedidoHub.Clients.Group(pedidoActual.Id).SendAsync(MethodsHub.UpdateOrder, _mapper.Map<PedidoDTO>(pedidoActual));
                    result.StatusCode = StatusCodes.Status400BadRequest;
                }

            }
            else
            {
                result.StatusCode = 401;
                result.Message = "NO_ROLE";
                await _smsServices.SendMessage(result.Message, result.StatusCode);
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
                await _pedidoHub.Clients.Group(pedidoActual.Id).SendAsync(MethodsHub.UpdateOrder, pedidoActual);
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
                        await _pedidoHub.Clients.Group(pedidoActual.Id).SendAsync(MethodsHub.UpdateOrder, _mapper.Map<PedidoDTO>(pedidoActual));
                    }
                    else
                    {
                        resultInfo = $@"ROL_NO_PERMISSION_CHANGE_STATUS ""{rolUsuario},{nuevaInformacion.Estado}""";
                        await _smsServices.SendMessage(resultInfo,StatusCodes.Status403Forbidden);
                        success = false;
                    }
                }
                else if (pedidoActual.Estado != EstadoPedido.Buscando)
                {
                    resultInfo = $@"ROL_NO_PERMISSION_CHANGE_STATUS ""{rolUsuario},{nuevaInformacion.Estado}""";
                    await _smsServices.SendMessage(resultInfo, StatusCodes.Status403Forbidden);
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
            var prodIds = nuevaInformacion.Detalles.Select(x => x.ProductoId).ToList();
            var orderId = nuevaInformacion.Id;

            #region SoftDeleted
            var softDeletedEntities = await _unitOfWork.DetallePedido.WhereSoftDeleted(x => x.PedidoId == orderId && prodIds.Contains(x.ProductoId)).ToListAsync();
            if (softDeletedEntities.Count > 0) softDeletedEntities.ForEach(x =>
            {
                x.DeletedAt = null;
                x.Cantidad = nuevaInformacion.Detalles.First(y => y.ProductoId == x.ProductoId).Cantidad;
            });
            var opReactivate = await _unitOfWork.DetallePedido.Update(softDeletedEntities);
            #endregion

            #region UpdateRegion
            var updatedEntities = pedidoActual.Detalles.Where(x =>
            {
                var data = nuevaInformacion.Detalles.FirstOrDefault(y => y.ProductoId == x.ProductoId);
                if(data != null && data.Cantidad != x.Cantidad && data.Cantidad > 0 && (softDeletedEntities.Count > 0 ? softDeletedEntities.Any(y => y.ProductoId != x.ProductoId) : true))
                    return true;    
                return false;
            }).ToList();
            updatedEntities.ForEach(x =>
            {
                x.Cantidad = nuevaInformacion.Detalles.First(y => y.ProductoId == x.ProductoId).Cantidad;
            });
            var opUpdate = await _unitOfWork.DetallePedido.Update(updatedEntities);
            #endregion

            #region InsertRegion
            var insertedEntities = pedidoActual.Detalles.Where(x => nuevaInformacion.Detalles.FirstOrDefault(y => y.ProductoId == x.ProductoId) == null && softDeletedEntities.Any(y => y.ProductoId != x.ProductoId)).ToList();
            if (pedidoActual.Detalles.Count == 0) foreach (var d in nuevaInformacion.Detalles) insertedEntities.Add(d);
            var opInserted = await _unitOfWork.DetallePedido.Insert(insertedEntities);
            #endregion

            #region DeleteRegion
            var deletedEntities = pedidoActual.Detalles.Where(x =>
            {
                var info = nuevaInformacion.Detalles.FirstOrDefault(y => y.ProductoId != x.ProductoId);
                if (info == null && !insertedEntities.Any(y => x.ProductoId == y.ProductoId)) return true;
                else if (info != null && info.Cantidad <= 0) return true;
                else return false;
            }).ToList();
            var opDelete = await _unitOfWork.DetallePedido.Delete(deletedEntities);
            #endregion

            return (opInserted, opUpdate, opDelete, opReactivate);
        }

        

        public async Task ObservarPedido(string pedidoId)
        {
            var pedido = await _unitOfWork.Pedido.Where(x => x.Id == pedidoId).FirstOrDefaultAsync();
            var cRole = _userServices.CurrentRol;
            var cUser = _userServices.CurrentId;
            var conId = _userServices.HubContext.ConnectionId;

            if (pedido != null)
            {
                if(pedido.UsuarioId == cUser
                    || (cRole == Roles.Delivery && pedido.Estado == EstadoPedido.Delivery || pedido.Estado == EstadoPedido.Puerta)
                    || (cRole == Roles.Cocinero && pedido.Estado == EstadoPedido.Preparando || pedido.Estado == EstadoPedido.Realizado)
                    || (cRole == Roles.Recepcionista || cRole == Roles.Administrador)
                 )
                {
                    await _pedidoHub.Groups.AddToGroupAsync(conId, pedidoId);
                    await _pedidoHub.Clients.Client(conId).SendAsync(MethodsHub.SendOperation, $@"ORDER_CONNECTED ""{pedidoId}{conId}""");
                }
            }
            else
            {
                await _pedidoHub.Clients.Client(conId).SendAsync(MethodsHub.SendMessage, $@"NOT_FOUND ""{pedidoId.Trim()}""");
            }
        }
    }
}
