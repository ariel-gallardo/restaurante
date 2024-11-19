using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        void SaveChanges();
        IRepository<Domicilio> Domicilio { get; }
        IRepository<Persona> Persona { get; }
        IRepository<Rol> Rol { get; }
        IRepository<Telefono> Telefono { get; }
        IUsuarioRepository Usuario { get; }
        IProductoRepository Producto { get; }
        IIngredienteRepository Ingrediente { get; }
        IProductoIngredienteRepository ProductoIngrediente { get; }
    }
}
