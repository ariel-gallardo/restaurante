using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public static class PersonaMapping
    {
        public static void Map<T>(this ModelBuilder modelBuilder) where T : Persona
        {
            modelBuilder.MapBaseBigInt<T>();
            modelBuilder.Entity<T>().Property(x => x.Nombre).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<T>().Property(x => x.Apellido).IsRequired().ValueGeneratedNever();
        }

        public static void MapRelationShips<T>(this ModelBuilder modelBuilder) where T : Persona
        {
            modelBuilder.Entity<T>().HasOne(x => x.Domicilio).WithMany().HasForeignKey(x => x.DomicilioId).IsRequired(false);
            modelBuilder.Entity<T>().HasOne(x => x.Telefono).WithMany().HasForeignKey(x => x.TelefonoId).IsRequired(false);
        }
    }
}
