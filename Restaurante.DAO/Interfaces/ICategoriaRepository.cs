using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> Crear(Categoria categoria);
        Task<bool> EditarCategory(Categoria? entity, Categoria newProperties);
        Task<Paginacion<Categoria>> ListarCategorias(int paginaNum = 1, bool ascendente = true, string nombreClave = "", string catPadreId = "");
    }
}
