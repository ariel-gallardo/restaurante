using AutoMapper;

namespace Restaurante.Models.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CrearCategoriaDTO, Categoria>()
                .ForMember(x => x.Nombre, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.Nombre) ? y.Nombre : null))
                .ForMember(x => x.Descripcion, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.Descripcion) ? y.Descripcion : null))
                .ForMember(x => x.CategoriaPadreId, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.CategoriaPadreId) ? y.CategoriaPadreId : null))
                .ForMember(x => x.CategoriaPadre, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.CategoriaPadreId) || !string.IsNullOrWhiteSpace(y.CategoriaPadre) ? new Categoria
                {
                    Id = !string.IsNullOrWhiteSpace(y.CategoriaPadreId) ? long.Parse(y.CategoriaPadreId) : 0,
                    Nombre = !string.IsNullOrWhiteSpace(y.CategoriaPadre) ? y.CategoriaPadre : null
                } : null));

            CreateMap<EditarCategoriaDTO, Categoria>()
                .ForMember(x => x.Nombre, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.Nombre) ? y.Nombre : null))
                .ForMember(x => x.Descripcion, x => x.MapFrom(y => !string.IsNullOrWhiteSpace(y.Descripcion) ? y.Descripcion : null))
                .ForMember(x => x.CategoriaPadreId, x => x.MapFrom(y => y.CategoriaPadreId.HasValue ? y.CategoriaPadreId : null));

            CreateMap<Categoria, Categoria>()
                .ForMember(x => x.CategoriaPadreId, x => x.MapFrom((src, dest) => src.CategoriaPadreId))
                .ForMember(x => x.Nombre, x => x.MapFrom((src, dest) => !string.IsNullOrWhiteSpace(src.Nombre) ? src.Nombre : dest.Nombre))
                .ForMember(x => x.Descripcion, x => x.MapFrom((src, dest) => src.Descripcion))
                .ForMember(x => x.CreatedAt, x => x.MapFrom((src, dest) => dest.CreatedAt))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom((src, dest) => dest.UpdatedAt))
                .ForMember(x => x.DeletedAt, x => x.MapFrom((src, dest) => dest.DeletedAt));
        }
    }
}
