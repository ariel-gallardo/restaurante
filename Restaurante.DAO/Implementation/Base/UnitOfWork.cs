

using Restaurante.Migrations;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private
        private readonly RestauranteContext _ctx;
        private readonly IRepository<Domicilio> _domicilioRepository;
        private readonly IRepository<Persona> _personaRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<Telefono> _telefonoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        #endregion

        #region Public
        public IRepository<Domicilio> Domicilio { get => _domicilioRepository; }
        public IRepository<Persona> Persona { get => _personaRepository; }
        public IRepository<Rol> Rol { get => _rolRepository; }
        public IRepository<Telefono> Telefono { get => _telefonoRepository; }
        public IUsuarioRepository Usuario { get => _usuarioRepository; }
        #endregion

        #region Constructor
        public UnitOfWork(
            RestauranteContext ctx,
            IRepository<Domicilio> domicilioRepository,
            IRepository<Persona> personaRepository,
            IRepository<Rol> rolRepository,
            IRepository<Telefono> telefonoRepository,
            IUsuarioRepository usuarioRepository
            )
        {
            _domicilioRepository = domicilioRepository;
            _personaRepository = personaRepository;
            _rolRepository = rolRepository;
            _telefonoRepository = telefonoRepository;
            _usuarioRepository = usuarioRepository;
            _ctx = ctx;
        }
        #endregion
        public void Dispose()
        {
            _ctx.Dispose();
        }
        public async Task SaveChangesAsync()
        {
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                
            }
        }
    }
}
