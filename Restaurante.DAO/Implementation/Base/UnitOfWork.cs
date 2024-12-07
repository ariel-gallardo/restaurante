using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IRepository<DetallePedido> _detallePedidoRepository;
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

        public IPedidoRepository Pedido { get => _pedidoRepository; }
        public IRepository<DetallePedido> DetallePedido => _detallePedidoRepository;

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
            IProductoIngredienteRepository productoIngredienteRepository,
            IPedidoRepository pedidoRepository,
            IRepository<DetallePedido> detallePedidoRepository
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
            _pedidoRepository = pedidoRepository;
            _detallePedidoRepository = detallePedidoRepository;
            _ctx = ctx;
            AssignUnitOfWork();
        }
        #endregion
        public void Dispose()
        {
            _transaction?.Dispose();
            _ctx.Dispose();
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
            _pedidoRepository.UnitOfWork = this;
        }

        private IDbContextTransaction _transaction;

        public IDbContextTransaction Transaction { get => _transaction; }


        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _ctx.Database.BeginTransaction();
            }
        }
        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _ctx.Database.BeginTransactionAsync();
            }
        }
        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                try
                {
                    _transaction.Commit();
                }
                catch (Exception ex) 
                {
                    RollbackTransaction();
                    throw ex;
                }
                finally
                {
                    
                }
            }
        }
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                try
                {
                    await _transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await RollbackTransactionAsync();
                    throw ex;
                }
                finally
                {
                    
                }
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void SaveChanges() => _ctx.SaveChanges();

        public async Task SaveChangesAsync() => await _ctx.SaveChangesAsync();
        
    }
}
