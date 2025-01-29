using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class ProductoIngredienteRepository : BaseRepository<ProductoIngrediente>, IProductoIngredienteRepository
    {
        public ProductoIngredienteRepository(IRepository<ProductoIngrediente> repository) : base(repository)
        {
        }

        public async Task<Paginacion<ProductoIngrediente>> ListarProductoIngrediente(int? paginaNum = 1, string? productoId = "", string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0, double? precioMax = 0, bool? porPrecioVenta = true)
        {
            (var count, var querie) = WhereAsPaginateQuerie(x =>
            !string.IsNullOrWhiteSpace(productoId) ? x.Id == productoId : true
            && !string.IsNullOrWhiteSpace(nombreClave) ? EF.Functions.Like(x.Ingrediente.Nombre, $"%{nombreClave}%") : true
                && (precioMin > 0.0 ? (porPrecioVenta.Value ? x.Ingrediente.PrecioVenta >= precioMin : x.Ingrediente.PrecioCompra >= precioMin) : true || precioMax > 0.0 ? (porPrecioVenta.Value ? x.Ingrediente.PrecioVenta <= precioMax : x.Ingrediente.PrecioCompra >= precioMax) : true),
                x => !string.IsNullOrEmpty(ordenarPor) ?
                ordenarPor.Contains("precioCompra") ? x.Ingrediente.PrecioCompra
                : (
                    ordenarPor.Contains("precioVenta") ? x.Ingrediente.PrecioVenta
                    : x.Ingrediente.Nombre
                ) : x.Ingrediente.Nombre
                , ascendente.Value, paginaNum.Value
            );

            var results = await querie.Include(x => x.Ingrediente).ToListAsync();
            return Paginacion<ProductoIngrediente>.Crear(results, count, paginaNum.HasValue ? paginaNum.Value : 1);
        }
    }
}
