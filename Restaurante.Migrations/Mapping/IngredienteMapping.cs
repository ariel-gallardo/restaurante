using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class IngredienteMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Ingrediente
        {
            modelBuilder.MapBaseString<T>();
            modelBuilder.Entity<T>().Property(x => x.ImagenUrl).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.StockActual).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.StockAlerta).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.PrecioCompra).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.PrecioVenta).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.Unidad);
            modelBuilder.Entity<T>().Property(x => x.Descripcion).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.Nombre);
            modelBuilder.Entity<T>().HasIndex(x => x.Unidad);
            modelBuilder.Entity<T>().HasIndex(x => x.Nombre);
            modelBuilder.Entity<T>().HasIndex(x => x.ImagenUrl);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Ingrediente
        {

        }
    }
}
