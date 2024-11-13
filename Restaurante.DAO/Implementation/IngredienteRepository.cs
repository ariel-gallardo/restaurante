using Restaurante.Models;

namespace Restaurante.DAO
{
    public class IngredienteRepository : BaseRepository<Ingrediente>, IIngredienteRepository
    {
        public IngredienteRepository(IRepository<Ingrediente> repository) : base(repository)
        {
        }
    }
}
