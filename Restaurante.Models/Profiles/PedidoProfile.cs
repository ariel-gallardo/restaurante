using AutoMapper;

namespace Restaurante.Models.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<PedidoDTO.PedidoDTOData, DetallePedido>()
                .ForMember(x => x.ProductoId, x => x.MapFrom(y => y.ProductoId))
                .ForMember(x => x.Cantidad, x => x.MapFrom(y => y.Cantidad));
        }
    }
}
