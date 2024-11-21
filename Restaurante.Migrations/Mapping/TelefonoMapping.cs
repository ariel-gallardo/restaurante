using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class TelefonoMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Telefono
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.CodigoArea).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Numero).IsRequired().ValueGeneratedNever();
        }
    }
}
