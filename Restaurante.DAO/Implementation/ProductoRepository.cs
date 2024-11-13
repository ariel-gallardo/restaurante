using Restaurante.Models;

namespace Restaurante.DAO
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(IRepository<Producto> repository) : base(repository)
        {
        }
    }
}
