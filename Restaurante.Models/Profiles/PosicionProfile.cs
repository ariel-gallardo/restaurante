using AutoMapper;

namespace Restaurante.Models.Profiles
{
    public class PosicionProfile : Profile
    {
        public PosicionProfile()
        {
            CreateMap<PosicionDTO, Posicion>()
                .ForMember(x => x.Tiempo, x => x.MapFrom(y => y.Fecha))
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Latitud))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Longitud))
                .ForMember(x => x.Velocidad, x => x.MapFrom(y => y.Velocidad))
                .ForMember(x => x.Direccion, x => x.MapFrom(y => y.Direccion))
                .ForMember(x => x.PedidoId, x => x.MapFrom(y => y.PedidoId));
        }
    }
}
