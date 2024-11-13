using Restaurante.Models;

namespace Restaurante.DAO
{
    public class ProductoIngredienteRepository : BaseRepository<ProductoIngrediente>, IProductoIngredienteRepository
    {
        public ProductoIngredienteRepository(IRepository<ProductoIngrediente> repository) : base(repository)
        {
        }
    }
}
