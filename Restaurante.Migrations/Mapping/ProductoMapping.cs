using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class ProductoMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Producto
        {
            modelBuilder.MapBaseString<T>();
            modelBuilder.Entity<T>().Property(x => x.ImagenUrl);
            modelBuilder.Entity<T>().Property(x => x.StockActual);
            modelBuilder.Entity<T>().Property(x => x.StockAlerta);
            modelBuilder.Entity<T>().Property(x => x.PrecioCompra);
            modelBuilder.Entity<T>().Property(x => x.PrecioVenta);
            modelBuilder.Entity<T>().Property(x => x.Unidad);
            modelBuilder.Entity<T>().HasIndex(x => x.Unidad);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Producto
        {

        }
    }
}
