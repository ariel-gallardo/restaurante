using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<bool> ExistsProducto(string nombreProducto);
        Task<Producto> CrearProducto(Producto entity);
        Task<bool> EditarProducto(Producto entity, Producto newProperties);
        Task<Paginacion<Producto>> ListarProductos(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0);
    }
}
