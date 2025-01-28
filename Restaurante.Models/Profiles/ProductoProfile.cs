using AutoMapper;

namespace Restaurante.Models.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<CrearProductoDTO, Producto>()
            .ForMember(y => y.Ingredientes, x => x.MapFrom(xx =>
                xx.Ingredientes != null
                ? xx.Ingredientes.Select(yy =>
                        new ProductoIngrediente
                        {
                            IngredienteId = yy.Item1,
                            Cantidad = yy.Item2,
                            Unidad = yy.Item3,
                        }
                    )
                : null
            ))
            .ForMember(y => y.Unidad, x => x.MapFrom(xx => xx.Unidad))
            .ForMember(y => y.StockActual, x => x.MapFrom(xx =>
                xx.Ingredientes.Length > 0 ?
                null : xx.StockActual.HasValue ? xx.StockActual : null)
            ).ForMember(y => y.StockAlerta, x => x.MapFrom(xx =>
                xx.Ingredientes.Length > 0 ?
                null : xx.StockAlerta.HasValue ? xx.StockAlerta : null)
            ).ForMember(y => y.PrecioCompra, x => x.MapFrom(xx => xx.PrecioDeCompra.HasValue ? xx.PrecioDeCompra : null)
            ).ForMember(y => y.PrecioVenta, x => x.MapFrom(xx => xx.PrecioDeVenta.HasValue ? xx.PrecioDeVenta : null)
            ).ForMember(y => y.Descripcion, x => x.MapFrom(xx => xx.Descripcion)
            ).ForMember(y => y.CategoriaId, x => x.MapFrom(xx => xx.CategoriaId.HasValue ? xx.CategoriaId : null));

            CreateMap<EditarProductoDTO, Producto>()
            .ForMember(y => y.Ingredientes, x => x.MapFrom(xx =>
                xx.Ingredientes != null
                ? xx.Ingredientes.Select(yy =>
                        new ProductoIngrediente
                        {
                            IngredienteId = yy.Item1,
                            Cantidad = yy.Item2,
                            Unidad = yy.Item3,
                            ProductoId = xx.ProductoId
                        }
                    )
                : null
            ))
            .ForMember(y => y.Unidad, x => x.MapFrom(xx => !string.IsNullOrEmpty(xx.Unidad) ? xx.Unidad : null))
            .ForMember(y => y.StockActual, x => x.MapFrom(xx =>
                xx.Ingredientes.Length > 0 ?
                null : xx.StockActual.HasValue ? xx.StockActual : null)
            ).ForMember(y => y.StockAlerta, x => x.MapFrom(xx =>
                xx.Ingredientes.Length > 0 ?
                null : xx.StockAlerta.HasValue ? xx.StockAlerta : null)
            ).ForMember(y => y.PrecioCompra, x => x.MapFrom(xx => xx.PrecioDeCompra.HasValue ? xx.PrecioDeCompra : null)
            ).ForMember(y => y.PrecioVenta, x => x.MapFrom(xx => xx.PrecioDeVenta.HasValue ? xx.PrecioDeVenta : null)
            ).ForMember(y => y.Descripcion, x => x.MapFrom(xx => xx.Descripcion))
            .ForMember(y => y.CategoriaId, x => x.MapFrom(xx => xx.CategoriaId.HasValue ? xx.CategoriaId : null));

            CreateMap<Producto, Producto>()
                .ForMember(y => y.Ingredientes,
                    x => x.MapFrom(
                        (src, dest) =>
                        src.Ingredientes.Count > 0 ?
                        src.Ingredientes.Select(zz =>
                        new ProductoIngrediente
                        {
                            IngredienteId = zz.IngredienteId,
                            Cantidad = zz.Cantidad,
                            Unidad = zz.Unidad,
                            ProductoId = zz.ProductoId
                        })
                        : dest.Ingredientes
                    )
                )
                .ForMember(y => y.Unidad, x => x.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Unidad) ? src.Unidad : dest.Unidad))
                .ForMember(y => y.StockActual, x => x.MapFrom((src, dest) => src.Ingredientes.Count == 0 ? src.StockActual.HasValue ? src.StockActual : dest.StockActual : null))
                .ForMember(y => y.StockAlerta, x => x.MapFrom((src, dest) => src.Ingredientes.Count == 0 ? src.StockAlerta.HasValue ? src.StockAlerta : dest.StockAlerta : null))
                .ForMember(y => y.PrecioCompra, x => x.MapFrom((src, dest) => src.Ingredientes.Count == 0 ? src.PrecioCompra.HasValue ? src.PrecioCompra : dest.PrecioCompra : null))
                .ForMember(y => y.PrecioVenta, x => x.MapFrom((src, dest) => src.Ingredientes.Count == 0 ? src.PrecioVenta.HasValue ? src.PrecioVenta : dest.PrecioVenta : null))
                .ForMember(y => y.Descripcion, x => x.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Descripcion) ? src.Descripcion : dest.Descripcion))
                .ForMember(x => x.CreatedAt, x => x.MapFrom((src, dest) => dest.CreatedAt))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom((src, dest) => dest.UpdatedAt))
                .ForMember(x => x.DeletedAt, x => x.MapFrom((src, dest) => dest.DeletedAt))
                .ForMember(x => x.CategoriaId, x => x.MapFrom((src, dest) => src.CategoriaId.HasValue ? src.CategoriaId : dest.CategoriaId));
        }
    }
}
