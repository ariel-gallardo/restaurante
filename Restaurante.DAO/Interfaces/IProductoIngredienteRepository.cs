using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IProductoIngredienteRepository : IRepository<ProductoIngrediente>
    {
        Task<Paginacion<ProductoIngrediente>> ListarProductoIngrediente(int? paginaNum = 1, string? productoId = "", string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, bool? porPrecioVenta = true);
    }
}
