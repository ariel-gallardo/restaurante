using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario SearchUserActiveByEmail(string email);
        Task<Usuario> SearchUserActiveByEmailWithDelivery(string email);
    }
}
