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
            modelBuilder.Entity<T>().HasIndex(x => x.Unidad);
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Ingrediente
        {

        }
    }
}
