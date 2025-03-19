using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Paginacion<Pedido>> BuscarActualesPorRol(long userId, string rol, bool asc, int page = 1);
        Task<Pedido> PedidoActual(long userId);
        Task<long?> ClienteId(string pedidoId);
        Task<long?> DeliveryId(string pedidoId);
        Task<long?> SeleccionarDelivery(string pedidoId, long clienteId, long deliveryId);
    }
}
