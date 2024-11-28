using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPedidoServices
    {
        Task<ResultResponse> InteractuarPedidoActual(PedidoDTO dTO);
    }
}
