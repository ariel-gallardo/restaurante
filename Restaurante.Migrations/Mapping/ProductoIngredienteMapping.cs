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
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : ProductoIngrediente
        {

        }
    }
}
