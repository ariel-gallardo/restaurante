using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        IRepository<Domicilio> Domicilio { get; }
        IRepository<Persona> Persona { get; }
        IRepository<Rol> Rol { get; }
        IRepository<Telefono> Telefono { get; }
        IUsuarioRepository Usuario { get; }
    }
}
