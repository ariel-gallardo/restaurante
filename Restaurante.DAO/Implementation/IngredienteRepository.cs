using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class IngredienteRepository : BaseRepository<Ingrediente>, IIngredienteRepository
    {
        public IngredienteRepository(IRepository<Ingrediente> repository) : base(repository)
        {
        }

        public async Task<Paginacion<Ingrediente>> ListarIngredientes(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0, double? precioMax = 0, bool? porPrecioVenta = true)
        =>  await WhereAsPaginateListAsync(x =>
                !string.IsNullOrEmpty(nombreClave) ? EF.Functions.Like(x.Nombre, $"%{nombreClave}%") : true
                && (precioMin > 0.0 ? (porPrecioVenta.Value ? x.PrecioVenta >= precioMin : x.PrecioCompra >= precioMin) : true || precioMax > 0.0 ? (porPrecioVenta.Value ? x.PrecioVenta <= precioMax : x.PrecioCompra <= precioMax) : true),
                x => !string.IsNullOrEmpty(ordenarPor) ?
                ordenarPor.Contains("precioCompra") ? x.PrecioCompra
                : (
                    ordenarPor.Contains("precioVenta") ? x.PrecioVenta
                    : x.Nombre
                ) : x.Nombre
                , ascendente.Value, paginaNum.Value
            );
        
    }
}
