using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations.Extensions;
using Restaurante.Models;

namespace Restaurante.Migrations
{
    public partial class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) :
            base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder mB)
        {
            base.OnModelCreating(mB);
            MapEntities(ref mB);
            MapRelationShips(ref mB);
            GenerateDemoData(ref mB);
        }

        private void MapEntities(ref ModelBuilder mB)
        {
            mB.Map<Rol>();
            mB.Map<Domicilio>();
            mB.Map<Telefono>();
            mB.Map<Persona>();
            mB.Map<Usuario>();
            mB.Map<Ingrediente>();
            mB.Map<ProductoIngrediente>();
            mB.Map<Producto>();
            mB.Map<DetallePedido>();
            mB.Map<Pedido>();
        }

        private void MapRelationShips(ref ModelBuilder mB)
        {
            mB.MapRelationShips<Persona>();
            mB.MapRelationShips<Usuario>();
            mB.MapRelationShips<Producto>();
            mB.MapRelationShips<ProductoIngrediente>();
        }

        private void GenerateDemoData(ref ModelBuilder mB)
        {
            mB.GenerateDemoRoles()
            .GenerateDemoDomicilios()
            .GenerateDemoTelefonos()
            .GenerateDemoPersonas()
            .GenerateDemoUsers();
        }

        public override int SaveChanges()
        {

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
                if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                    entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
                if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                    entity.Property("DeletedAt").CurrentValue = DateTime.UtcNow;

            return base.SaveChanges();
        }
    }
}
