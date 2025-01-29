using Restaurante.Migrations;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        public RestauranteContext Context { get; }
        void ClearChanges();
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
        ICategoriaRepository Categoria { get; }
    }
}
