using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUsuarioRepository
    {
        Usuario SearchUserActiveByEmail(string email);
        IRepository<Usuario> Repository { get; }
    }
}
