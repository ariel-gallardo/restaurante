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
        private readonly IRepository<Usuario> _b_usuarioRepository;
        private readonly IRepository<Producto> _b_productoRepository;
        private readonly IRepository<Ingrediente> _b_ingredienteRepository;
        private readonly IRepository<ProductoIngrediente> _b_pIngredienteRepository;
        private readonly IRepository<Pedido> _b_pedidoRepository;
        private readonly IRepository<Categoria> _b_categoriaRepository;
        private readonly IRepository<Domicilio> _b_domicilioRepository;
        private readonly IRepository<Persona> _b_personaRepository;
        private readonly IRepository<Rol> _b_rolRepository;
        private readonly IRepository<Telefono> _b_telefonoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IProductoIngredienteRepository _productoIngredienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IRepository<DetallePedido> _detallePedidoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        #endregion

        #region Public
        public IRepository<Domicilio> Domicilio { get => _b_domicilioRepository; }
        public IRepository<Persona> Persona { get => _b_personaRepository; }
        public IRepository<Rol> Rol { get => _b_rolRepository; }
        public IRepository<Telefono> Telefono { get => _b_telefonoRepository; }
        public IUsuarioRepository Usuario { get => _usuarioRepository; }
        public IProductoRepository Producto { get => _productoRepository; }
        public IIngredienteRepository Ingrediente { get => _ingredienteRepository; }
        public IProductoIngredienteRepository ProductoIngrediente { get => _productoIngredienteRepository; }
        public IPedidoRepository Pedido { get => _pedidoRepository; }
        public IRepository<DetallePedido> DetallePedido => _detallePedidoRepository;
        public ICategoriaRepository Categoria { get => _categoriaRepository; }
        public RestauranteContext Context { get => _ctx; }
        #endregion

        #region Constructor
        public UnitOfWork(
            RestauranteContext ctx,
            IRepository<Domicilio> b_domicilioRepository,
            IRepository<Persona> b_personaRepository,
            IRepository<Rol> b_rolRepository,
            IRepository<Telefono> b_telefonoRepository,
            IRepository<Usuario> b_usuarioRepository,
            IRepository<Producto> b_productoRepository,
            IRepository<Ingrediente> b_ingredienteRepository,
            IRepository<ProductoIngrediente> b_pIngredienteRepository,
            IRepository<Pedido> b_pedidoRepository,
            IRepository<Categoria> b_categoriaRepository,
            IUsuarioRepository usuarioRepository,
            IProductoRepository productoRepository,
            IIngredienteRepository ingredienteRepository,
            IProductoIngredienteRepository productoIngredienteRepository,
            IPedidoRepository pedidoRepository,
            IRepository<DetallePedido> detallePedidoRepository,
            ICategoriaRepository categoriaRepository
            )
        {
            _ctx = ctx;
            _b_usuarioRepository = b_usuarioRepository;
            _b_productoRepository = b_productoRepository;
            _b_ingredienteRepository = b_ingredienteRepository;
            _b_pIngredienteRepository = b_pIngredienteRepository;
            _b_pedidoRepository = b_pedidoRepository;
            _b_categoriaRepository = b_categoriaRepository;
            _b_domicilioRepository = b_domicilioRepository;
            _b_personaRepository = b_personaRepository;
            _b_rolRepository = b_rolRepository;
            _b_telefonoRepository = b_telefonoRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _ingredienteRepository = ingredienteRepository;
            _productoIngredienteRepository = productoIngredienteRepository;
            _pedidoRepository = pedidoRepository;
            _detallePedidoRepository = detallePedidoRepository;
            _categoriaRepository = categoriaRepository;
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
            _b_usuarioRepository.UnitOfWork = this;
            _b_productoRepository.UnitOfWork = this;
            _b_ingredienteRepository.UnitOfWork = this;
            _b_pIngredienteRepository.UnitOfWork = this;
            _b_pedidoRepository.UnitOfWork = this;
            _b_categoriaRepository.UnitOfWork = this;
            _b_domicilioRepository.UnitOfWork = this;
            _b_personaRepository.UnitOfWork = this;
            _b_rolRepository.UnitOfWork = this;
            _b_telefonoRepository.UnitOfWork = this;
            _usuarioRepository.UnitOfWork = this;
            _productoRepository.UnitOfWork = this;
            _ingredienteRepository.UnitOfWork = this;
            _productoIngredienteRepository.UnitOfWork = this;
            _pedidoRepository.UnitOfWork = this;
            _detallePedidoRepository.UnitOfWork = this;
            _categoriaRepository.UnitOfWork = this;
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
