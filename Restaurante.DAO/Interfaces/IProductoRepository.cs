using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<bool> ExistsProducto(string nombreProducto);
        Task<Producto> CrearProducto(Producto entity);
        Task<bool> EditarProducto(Producto entity, Producto newProperties);
        Task<(IList<Producto>, int)> ListarProductos(Expression<Func<Producto, bool>> whereExpression, int page);
    }
}
