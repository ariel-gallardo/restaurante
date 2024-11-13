using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(IRepository<Usuario> repository) : base(repository)
        {
        }
        public Usuario SearchUserActiveByEmail(string email) 
        {
            var usuario = Where(u => u.DeletedAt == null && u.Email == email)
                .Include(u => u.Persona)
                .Include(u => u.Persona.Domicilio)
                .Include(u => u.Persona.Telefono)
                .Include(u => u.Rol)
                .FirstOrDefault();
            return usuario;
        }
    }
}
