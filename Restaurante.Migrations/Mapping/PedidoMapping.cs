using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class PedidoMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Pedido
        {
            modelBuilder.MapBaseString<T>();
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Pedido
        {
            modelBuilder.Entity<T>().HasMany(x => x.Detalles).WithOne().HasForeignKey(x => x.PedidoId);
        }
    }
}
