using AutoMapper;

namespace Restaurante.Models.Profiles
{
    public class IngredienteProfile : Profile
    {
        public IngredienteProfile()
        {
            CreateMap<CrearIngredienteDTO, Ingrediente>()
                .ForMember(x => x.Nombre, x => x.MapFrom(xx => xx.Nombre))
                .ForMember(x => x.Descripcion, x => x.MapFrom(xx => xx.Descripcion))
                .ForMember(x => x.StockActual, x => x.MapFrom(xx => xx.StockActual))
                .ForMember(x => x.StockAlerta, x => x.MapFrom(xx => xx.StockAlerta))
                .ForMember(x => x.PrecioCompra, x => x.MapFrom(xx => xx.PrecioDeCompra))
                .ForMember(x => x.Unidad, x => x.MapFrom(xx => xx.Unidad))
                .ForMember(x => x.PrecioVenta, x => x.MapFrom(xx => xx.PrecioDeVenta));

            CreateMap<EditarIngredienteDTO, Ingrediente>()
                .ForMember(x => x.Nombre, x => x.MapFrom(xx => xx.Nombre))
                .ForMember(x => x.Descripcion, x => x.MapFrom(xx => xx.Descripcion))
                .ForMember(x => x.StockActual, x => x.MapFrom(xx => xx.StockActual))
                .ForMember(x => x.StockAlerta, x => x.MapFrom(xx => xx.StockAlerta))
                .ForMember(x => x.PrecioCompra, x => x.MapFrom(xx => xx.PrecioDeCompra))
                .ForMember(x => x.PrecioVenta, x => x.MapFrom(xx => xx.PrecioDeVenta))
                .ForMember(x => x.Unidad, x => x.MapFrom(xx => xx.Unidad))
                .ForMember(x => x.Id, x => x.MapFrom(xx => xx.IngredienteId));

            CreateMap<Ingrediente, Ingrediente>()
                .ForMember(x => x.Id, x => x.MapFrom((src, dest) => src.Id))
                .ForMember(x => x.Nombre, x => x.MapFrom((src, dest) => !String.IsNullOrWhiteSpace(src.Nombre) ? src.Nombre : dest.Nombre))
                .ForMember(x => x.Descripcion, x => x.MapFrom((src, dest) => !String.IsNullOrWhiteSpace(src.Descripcion) ? src.Descripcion : dest.Descripcion))
                .ForMember(x => x.PrecioCompra, x => x.MapFrom((src, dest) => src.PrecioCompra.HasValue && src.PrecioCompra > 0.0 ? src.PrecioCompra : dest.PrecioCompra))
                .ForMember(x => x.PrecioVenta, x => x.MapFrom((src, dest) => src.PrecioVenta.HasValue && src.PrecioVenta > 0.0 ? src.PrecioVenta : dest.PrecioVenta))
                .ForMember(x => x.StockActual, x => x.MapFrom((src, dest) => src.StockActual.HasValue && src.StockActual > 0.0 ? src.StockActual : dest.StockActual))
                .ForMember(x => x.StockAlerta, x => x.MapFrom((src, dest) => src.StockAlerta.HasValue && src.StockAlerta > 0.0 ? src.StockAlerta : dest.StockAlerta))
                .ForMember(x => x.Unidad, x => x.MapFrom((src, dest) => !String.IsNullOrWhiteSpace(src.Unidad) ? src.Unidad : dest.Unidad))
                .ForMember(x => x.CreatedAt, x => x.MapFrom((src, dest) => dest.CreatedAt))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom((src, dest) => dest.UpdatedAt))
                .ForMember(x => x.DeletedAt, x => x.MapFrom((src, dest) => dest.DeletedAt));
        }
    }
}
