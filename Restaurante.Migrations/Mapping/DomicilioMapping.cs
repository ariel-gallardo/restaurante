using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class DomicilioMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Domicilio
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.Calle).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Localidad).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Numero).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Latitud).IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.Longitud).IsRequired(false);
        }
    }
}
