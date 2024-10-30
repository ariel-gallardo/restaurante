using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class RolMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Rol
        {
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(x => x.Descripcion).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasIndex(x => x.Descripcion).IsUnique(true);
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
        }
    }
}
