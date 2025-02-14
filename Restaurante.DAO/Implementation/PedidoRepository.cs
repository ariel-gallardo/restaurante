using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Restaurante.Models.Enums;

namespace Restaurante.DAO
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(IRepository<Pedido> repository) : base(repository)
        {
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
    }
}
