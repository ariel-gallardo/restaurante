using Microsoft.Extensions.DependencyInjection;
using Restaurante.Models;

namespace Restaurante.DAO.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services =
             services
             .AddScoped<IRepository<Domicilio>, BigIntRepository<Domicilio>>()
             .AddScoped<IRepository<Persona>, BigIntRepository<Persona>>()
             .AddScoped<IRepository<Rol>, BigIntRepository<Rol>>()
             .AddScoped<IRepository<Telefono>, BigIntRepository<Telefono>>()
             .AddScoped<IRepository<Usuario>, BigIntRepository<Usuario>>()
             .AddScoped<IRepository<Ingrediente>, StringRepository<Ingrediente>>()
             .AddScoped<IRepository<Producto>, StringRepository<Producto>>()
             .AddScoped<IRepository<ProductoIngrediente>, StringRepository<ProductoIngrediente>>()
             .AddScoped<IRepository<Categoria>, BigIntRepository<Categoria>>();

            services =
                services
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<IProductoRepository, ProductoRepository>()
                .AddScoped<IIngredienteRepository, IngredienteRepository>()
                .AddScoped<IProductoIngredienteRepository, ProductoIngredienteRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>();



             return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
