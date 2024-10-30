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
        }

        private void MapRelationShips(ref ModelBuilder mB)
        {
            mB.MapRelationShips<Persona>();
            mB.MapRelationShips<Usuario>();
        }

        private void GenerateDemoData(ref ModelBuilder mB)
        {
            mB.GenerateDemoRoles()
            .GenerateDemoDomicilios()
            .GenerateDemoTelefonos()
            .GenerateDemoPersonas()
            .GenerateDemoUsers();
        }
    }
}
