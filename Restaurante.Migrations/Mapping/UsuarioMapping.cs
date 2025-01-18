using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class UsuarioMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Usuario
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.Email).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Password).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasIndex(x => x.Email).IsUnique(true);
            modelBuilder.Entity<T>().Property(x => x.RolId).HasDefaultValue(1L);
            modelBuilder.Entity<T>().Property(x => x.ImagenUrl).IsRequired(false);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Usuario
        {
            modelBuilder.Entity<T>().HasOne(x => x.Persona);
            modelBuilder.Entity<T>().HasOne(x => x.Rol).WithMany().HasForeignKey(x => x.RolId).IsRequired(false);
        }
    }
}
