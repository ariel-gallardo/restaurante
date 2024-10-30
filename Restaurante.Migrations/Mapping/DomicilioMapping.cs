using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class DomicilioMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Domicilio
        {
            modelBuilder.Entity<T>().Property(x => x.Calle).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Localidad).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Numero).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
        }
    }
}
