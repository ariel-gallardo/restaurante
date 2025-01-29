using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class CategoriaMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Categoria
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.Nombre);
            modelBuilder.Entity<T>().Property(x => x.Descripcion).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.CategoriaPadreId).IsRequired(false);
            modelBuilder.Entity<T>().HasIndex(x => x.Nombre);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Categoria
        {
            modelBuilder.Entity<T>()
                .HasOne(x => x.CategoriaPadre)
                .WithMany()
                .HasForeignKey(x => x.CategoriaPadreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
