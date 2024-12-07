using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Domicilio> Domicilio { get; }
        IRepository<Persona> Persona { get; }
        IRepository<Rol> Rol { get; }
        IRepository<Telefono> Telefono { get; }
        IUsuarioRepository Usuario { get; }
        IProductoRepository Producto { get; }
        IIngredienteRepository Ingrediente { get; }
        IProductoIngredienteRepository ProductoIngrediente { get; }
        IPedidoRepository Pedido { get; }
        IRepository<DetallePedido> DetallePedido { get; }
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
