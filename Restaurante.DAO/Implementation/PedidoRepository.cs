using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Restaurante.Const;
using Restaurante.Infraestructure;
using Restaurante.Models;
using Restaurante.Models.Enums;

namespace Restaurante.DAO
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IDistributedCache _cache;

        public PedidoRepository(IRepository<Pedido> repository, IDistributedCache cache) : base(repository)
        {
            _cache = cache;
        }

        public async Task<Pedido> PedidoActual(long userId)
        {
            var pedidoActual = await UnitOfWork.Pedido.Where(x => x.UsuarioId == userId && x.Estado != EstadoPedido.Cancelado && x.Estado != EstadoPedido.Entregado)
                .Include(x => x.Detalles)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (pedidoActual == null)
            {
                pedidoActual = new Pedido { UsuarioId = userId, Estado = EstadoPedido.Buscando };
                await Insert(pedidoActual);
            }
            return pedidoActual;
        }

        public async Task<long?> ClienteId(string pedidoId)
        {
            long? result = null;
            string cIdText = await _cache.GetStringAsync($"Pedido_Cliente_{pedidoId}");

            if (string.IsNullOrEmpty(cIdText))
            {
                result = await Where(x => x.Id == pedidoId).Select(x => x.UsuarioId).FirstOrDefaultAsync();
                if (result != null) await _cache.SetStringAsync($"Pedido_Cliente_{pedidoId}", $"{result}");
            }
            else
            {
                result = JsonSerializer.Deserialize<long?>(cIdText);
            }

            return result;
        }

        public async Task<long?> DeliveryId(string pedidoId)
        {
            var fromCache = await _cache.GetStringAsync($"Pedido_Delivery_{pedidoId}");
            if (!string.IsNullOrWhiteSpace(fromCache))
                return long.Parse(fromCache);
            else
            {
                var lastPosition = await UnitOfWork.Posicion.PosicionActual(pedidoId);
                if (lastPosition != null)
                {
                    await _cache.SetStringAsync($"Pedido_Delivery_{pedidoId}", $"{lastPosition.DeliveryId}");
                    return lastPosition.DeliveryId;
                }
            }
            return null;
        }

        public async Task<long?> SeleccionarDelivery(string pedidoId, long clienteId, long deliveryId)
        {
            var cPedido = await Where(x => x.UsuarioId == clienteId).FirstOrDefaultAsync();
            if (cPedido != null)
            {
                var uDelivery = await UnitOfWork.Usuario.Where(x => x.Rol.Descripcion == Roles.Delivery && x.Id == deliveryId).FirstOrDefaultAsync();
                if (uDelivery != null) {
                    cPedido.DeliveryId = deliveryId;
                    cPedido.Estado = EstadoPedido.Delivery;
                    await Task.WhenAll(
                        Update(cPedido),
                        _cache.SetStringAsync($"Pedido_Delivery_{pedidoId}", $"{deliveryId}")
                    );
                    return deliveryId;
                }

            }
            return null;
        }

        public async Task<Paginacion<Pedido>> BuscarActualesPorRol(long userId, string rol, bool asc, int page = 1)
        => await WhereAsPaginateListAsync(x =>
         (rol == Roles.Delivery && ParametroDeConfiguracion.EstadosPosiblesDelivery.Contains(x.Estado) && x.Delivery.Id == userId)
         || (rol == Roles.Cocinero && ParametroDeConfiguracion.EstadosPosiblesCocinero.Contains(x.Estado))
         || (rol == Roles.Recepcionista && ParametroDeConfiguracion.EstadosPosiblesRecepcionista.Contains(x.Estado))
         || (rol == Roles.Administrador)
        , x => x.CreatedAt, asc, page);
    }
}
