using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioRepository(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public IRepository<Usuario> Repository => _repository;

        public Usuario SearchUserActiveByEmail(string email) 
        {
            var usuario = _repository
                .Where(u => u.DeletedAt == null && u.Email == email)
                .Include(u => u.Persona)
                .Include(u => u.Persona.Domicilio)
                .Include(u => u.Persona.Telefono)
                .Include(u => u.Rol)
                .FirstOrDefault();
            return usuario;
        }
    }
}
