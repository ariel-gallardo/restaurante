using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class TelefonoMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Telefono
        {
            modelBuilder.Entity<T>().Property(x => x.CodigoArea).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Numero).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
        }
    }
}
