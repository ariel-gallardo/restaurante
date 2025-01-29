using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.Infraestructure;
using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(IRepository<Producto> repository) : base(repository)
        {
        }

        public async Task<bool> ExistsProducto(string nombreProducto)
        => await WhereActive(x => EF.Functions.Like(x.Nombre, nombreProducto)).CountAsync() > 0;

        public async Task<Producto> CrearProducto(Producto entity)
        {
            if (!await ExistsProducto(entity.Nombre))
            {
                await Insert(entity);
                await UnitOfWork.ProductoIngrediente.Insert(entity.Ingredientes);
                await UnitOfWork.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<bool> EditarProducto(Producto entity, Producto newProperties)
        {
            if (entity != null)
            {
                Update(newProperties);
                UnitOfWork.ProductoIngrediente.Update(newProperties.Ingredientes.Where(x => entity.Ingredientes.Contains(x)));
                UnitOfWork.ProductoIngrediente.Delete(entity.Ingredientes.Where(x => !newProperties.Ingredientes.Contains(x) && x.CreatedAt != null));
                await UnitOfWork.ProductoIngrediente.Insert(newProperties.Ingredientes.Where(x => !entity.Ingredientes.Contains(x) && x.CreatedAt == null));
                await UnitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Producto> ProductoWithIngrediente(string id)
        => await WhereActive(x => x.Id == id).Include(x => x.Ingredientes).Include(x => x.Categoria).FirstOrDefaultAsync();

        public async Task<Paginacion<Producto>> ListarProductos(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, long? categoria = 0)
        {
            var resultList = new List<Producto>();
            Expression<Func<Producto, bool>> baseQuerie = x => true;


            (var total, var querie) = WhereAsPaginateQuerie(
                x =>
                    categoria != 0 ? x.Categoria.Id == categoria : true
                    && precioMin == 0.0 && precioMax == 0.0 ? true :
                    (x.Ingredientes.Count() > 0 && (
                        (precioMin > 0.0 && x.Ingredientes.Sum(y => y.Ingrediente.PrecioVenta ?? 0) * (1 + AppSettings.PorcentajeGanancia / 100) >= precioMin) &&
                        (precioMax > 0.0 && x.Ingredientes.Sum(y => y.Ingrediente.PrecioVenta ?? 0) * (1 + AppSettings.PorcentajeGanancia / 100) <= precioMax)
                    )
                    || (!x.Ingredientes.Any() &&
                        ((!x.PrecioVenta.HasValue || x.PrecioVenta >= precioMin && precioMin > 0.0) 
                        ||(!x.PrecioVenta.HasValue || x.PrecioVenta <= precioMax && precioMax > 0.0))
                    ))
                    && !string.IsNullOrWhiteSpace(nombreClave)
                    ? (
                        x.Nombre.Contains(nombreClave)
                        || x.Ingredientes.Any(i => i.Ingrediente.Nombre.Contains(nombreClave))
                    ) : true
                );

            Expression<Func<Producto, double?>> exOrderByPrecio = x =>
            ordenarPor.Contains("precioVenta", StringComparison.InvariantCultureIgnoreCase)
                    ?
                        (x.Ingredientes.Count() > 0
                        ? x.Ingredientes.Sum(i => i.Ingrediente.PrecioVenta ?? 0.0) * (1 + AppSettings.PorcentajeGanancia / 100)
                        : x.PrecioVenta.HasValue ? x.PrecioVenta : null)
                    : (
                        ordenarPor.Contains("precioCompra", StringComparison.InvariantCultureIgnoreCase)
                            ? x.Ingredientes.Count() > 0 ? x.Ingredientes.Sum(i => i.Ingrediente.PrecioCompra ?? 0.0)
                            : (x.PrecioCompra.HasValue ? x.PrecioCompra : null)
                        : null
            );

            if (ascendente.HasValue && ascendente.Value)
            {
                if (ordenarPor.Contains("nombre", StringComparison.InvariantCultureIgnoreCase))
                    resultList.AddRange(await querie.OrderBy(x => x.Nombre).ToListAsync());
                else
                    resultList.AddRange(await querie.OrderBy(exOrderByPrecio).ToListAsync());
            }else if(ascendente.HasValue && !ascendente.Value)
            {
                if (ordenarPor.Contains("nombre", StringComparison.InvariantCultureIgnoreCase))
                    resultList.AddRange(await querie.OrderByDescending(x => x.Nombre).ToListAsync());
                else
                    resultList.AddRange(await querie.OrderByDescending(exOrderByPrecio).ToListAsync());
            }
            else
                resultList.AddRange(await querie.Include(x => x.Categoria).ToListAsync());
            return Paginacion<Producto>.Crear(resultList, total, paginaNum.HasValue ? paginaNum.Value : 1);
        }
    }
}
