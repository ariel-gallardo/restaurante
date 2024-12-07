using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPedidoServices
    {
        Task<ResultResponse> InteractuarPedidoActual(PedidoDTO dTO);
        Task<(int, int, int, int)> ModificarArticulosPedidoEstadoBuscando(Pedido pedidoActual, Pedido nuevaInformacion);
        Task<(bool, string)> ModificarEstadoPedido(Pedido pedidoActual, Pedido nuevaInformacion, string rolUsuario);
    }
}
