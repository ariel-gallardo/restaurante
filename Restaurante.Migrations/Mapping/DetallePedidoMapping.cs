using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class DetallePedidoMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : DetallePedido
        {
            modelBuilder.MapBaseString<T>();
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : DetallePedido
        {

        }
    }
}
