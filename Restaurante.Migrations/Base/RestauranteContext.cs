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
            mB.Map<Categoria>();
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
            mB.MapRelationShips<Categoria>();
            mB.MapRelationShips<Producto>();
            mB.MapRelationShips<ProductoIngrediente>();
            mB.MapRelationShips<Pedido>();
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

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
                if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                    entity.Property("CreatedAt").CurrentValue = DateTime.UtcNow;

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Property("DeletedAt").CurrentValue == e.Property("DeletedAt").OriginalValue))
                if ((entity.Entity is BigIntEntity || entity.Entity is StringEntity))
                    entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
                if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                {
                    entity.Property("DeletedAt").CurrentValue = DateTime.UtcNow;
                    entity.State = EntityState.Modified;
                }

            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                entity.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Property("DeletedAt").CurrentValue == e.Property("DeletedAt").OriginalValue))
                if ((entity.Entity is BigIntEntity || entity.Entity is StringEntity))
                    entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;

            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
                if (entity.Entity is BigIntEntity || entity.Entity is StringEntity)
                {
                    entity.Property("DeletedAt").CurrentValue = DateTime.UtcNow;
                    entity.State = EntityState.Modified;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
