using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IIngredienteRepository : IRepository<Ingrediente>
    {
        Task<Paginacion<Ingrediente>> ListarIngredientes(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, bool? porPrecioVenta = true);
    }
}
