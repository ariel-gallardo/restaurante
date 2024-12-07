using AutoMapper;
using Restaurante.Models.Enums;

namespace Restaurante.Models.Profiles
{
    public class PedidoProfile : Profile
    {
        private static string[] currentStatus = new string[]
        {
            EstadoPedido.Buscando, EstadoPedido.Puerta,EstadoPedido.Recepcion,
            EstadoPedido.Preparando, EstadoPedido.Delivery, EstadoPedido.Cancelado,
            EstadoPedido.Realizado
        };
        public PedidoProfile()
        {
            CreateMap<PedidoDTO, Pedido>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Pedido.ToGuidString()))
            .ForMember(x => x.Estado, x => x.MapFrom(y => currentStatus.Contains(y.Estado) ? y.Estado : null))
            .ForMember(x => x.Detalles, x => x.MapFrom(y => y.Data.Select(z => new DetallePedido
            {
                Cantidad = z.Cantidad < 0 ? 0 : z.Cantidad,
                ProductoId = z.ProductoId.ToGuidString(),
                PedidoId = y.Pedido.ToGuidString()
            }).ToList()));
        }
    }
}
