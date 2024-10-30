using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class PersonaMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Persona
        {
            modelBuilder.Entity<T>().Property(x => x.Nombre).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Apellido).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
        }

        internal static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Persona
        {
            modelBuilder.Entity<T>().HasOne(x => x.Domicilio).WithMany().HasForeignKey(x => x.DomicilioId).IsRequired(false);
            modelBuilder.Entity<T>().HasOne(x => x.Telefono).WithMany().HasForeignKey(x => x.TelefonoId).IsRequired(false);
        }
    }
}
