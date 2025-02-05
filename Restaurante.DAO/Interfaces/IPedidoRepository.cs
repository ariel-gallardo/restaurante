using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> PedidoActual(long userId);
    }
}
