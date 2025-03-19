using Microsoft.AspNetCore.SignalR;
using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPedidoServices
    {
        string SmsGroupId { get; set; }
        Task<ResultResponse> BuscarActualesPorRol(bool asc, int page = 1);
        Task<ResultResponse> BuscarPorId(string id);
        Task<ResultResponse> InteractuarPedidoActual(PedidoDTO dTO);
        Task<(int, int, int, int)> ModificarArticulosPedidoEstadoBuscando(Pedido pedidoActual, Pedido nuevaInformacion);
        Task<(bool, string)> ModificarEstadoPedido(Pedido pedidoActual, Pedido nuevaInformacion, string rolUsuario);
        Task ObservarPedido(string pedidoId);
        Task SeleccionarDelivery(string pedidoId, long clienteId, long deliveryId);
    }
}
