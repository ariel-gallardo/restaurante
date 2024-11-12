using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class RolMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Rol
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.Descripcion).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().HasIndex(x => x.Descripcion).IsUnique(true);
        }
    }
}
