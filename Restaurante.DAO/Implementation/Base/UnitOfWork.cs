using Restaurante.Migrations;
using Restaurante.Models;
using System;

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
        private readonly IProductoRepository _productoRepository;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IProductoIngredienteRepository _productoIngredienteRepository;
        #endregion

        #region Public
        public IRepository<Domicilio> Domicilio { get => _domicilioRepository; }
        public IRepository<Persona> Persona { get => _personaRepository; }
        public IRepository<Rol> Rol { get => _rolRepository; }
        public IRepository<Telefono> Telefono { get => _telefonoRepository; }
        public IUsuarioRepository Usuario { get => _usuarioRepository; }

        public IProductoRepository Producto { get => _productoRepository; }
        public IIngredienteRepository Ingrediente { get => _ingredienteRepository; }
        public IProductoIngredienteRepository ProductoIngrediente { get => _productoIngredienteRepository; }
        #endregion

        #region Constructor
        public UnitOfWork(
            RestauranteContext ctx,
            IRepository<Domicilio> domicilioRepository,
            IRepository<Persona> personaRepository,
            IRepository<Rol> rolRepository,
            IRepository<Telefono> telefonoRepository,
            IUsuarioRepository usuarioRepository,
            IProductoRepository productoRepository,
            IIngredienteRepository ingredienteRepository,
            IProductoIngredienteRepository productoIngredienteRepository
            )
        {
            _domicilioRepository = domicilioRepository;
            _personaRepository = personaRepository;
            _rolRepository = rolRepository;
            _telefonoRepository = telefonoRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _ingredienteRepository = ingredienteRepository;
            _productoIngredienteRepository = productoIngredienteRepository;
            _ctx = ctx;
            AssignUnitOfWork();
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
                _ctx.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void SaveChanges()
        {
            try
            {
                 _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                _ctx.ChangeTracker.Clear();
                throw ex;
            }
        }
        private void AssignUnitOfWork()
        {
            _domicilioRepository.UnitOfWork = this;
            _personaRepository.UnitOfWork = this;
            _rolRepository.UnitOfWork = this;
            _telefonoRepository.UnitOfWork = this;
            _usuarioRepository.UnitOfWork = this;
            _productoRepository.UnitOfWork = this;
            _ingredienteRepository.UnitOfWork = this;
            _productoIngredienteRepository.UnitOfWork = this;
        }
    }
}
