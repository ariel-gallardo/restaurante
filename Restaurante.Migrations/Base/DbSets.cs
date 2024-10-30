using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    #pragma warning disable CS8618
    public partial class RestauranteContext
    {
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Domicilio> Domicilios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Telefono> Telefonos { get; set; }
    }
    #pragma warning restore CS8618

}
