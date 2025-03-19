using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class PosicionMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Posicion
        {
            modelBuilder.MapBaseString<T>();
            modelBuilder.Entity<T>().Property(x => x.Latitud).IsRequired();
            modelBuilder.Entity<T>().Property(x => x.Longitud).IsRequired();
            modelBuilder.Entity<T>().Property(x => x.Velocidad).IsRequired();
            modelBuilder.Entity<T>().Property(x => x.Direccion).IsRequired();
            modelBuilder.Entity<T>().Property(x => x.Tiempo).IsRequired();
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Posicion
        {
            modelBuilder.Entity<T>().HasOne(x => x.Delivery);
            modelBuilder.Entity<T>().HasOne(x => x.Pedido);
        }
    }
}
