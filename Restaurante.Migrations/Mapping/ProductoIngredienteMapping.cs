using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class ProductoIngredienteMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : ProductoIngrediente
        {
            modelBuilder.MapBaseString<T>();
            modelBuilder.Entity<T>().Property(x => x.Cantidad);
            modelBuilder.Entity<T>().Property(x => x.Unidad);
            modelBuilder.Entity<T>().HasIndex(x => x.Unidad);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : ProductoIngrediente
        {
            modelBuilder.Entity<ProductoIngrediente>().HasOne(x => x.Producto).WithMany(x => x.Ingredientes);
            modelBuilder.Entity<ProductoIngrediente>().HasOne(x => x.Ingrediente);
        }
    }
}
