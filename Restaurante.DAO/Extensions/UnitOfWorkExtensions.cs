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
             .AddScoped<IRepository<Categoria>, BigIntRepository<Categoria>>()
             .AddScoped<IRepository<Ingrediente>, StringRepository<Ingrediente>>()
             .AddScoped<IRepository<Producto>, StringRepository<Producto>>()
             .AddScoped<IRepository<ProductoIngrediente>, StringRepository<ProductoIngrediente>>()
             .AddScoped<IRepository<Pedido>, StringRepository<Pedido>>()
             .AddScoped<IRepository<DetallePedido>, StringRepository<DetallePedido>>()
             .AddScoped<IRepository<Posicion>, StringRepository<Posicion>>();

            services =
                services
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<IIngredienteRepository, IngredienteRepository>()
                .AddScoped<IProductoRepository, ProductoRepository>()
                .AddScoped<IProductoIngredienteRepository, ProductoIngredienteRepository>()
                .AddScoped<IPedidoRepository, PedidoRepository>()
                .AddScoped<IPosicionRepository, PosicionRepository>();


             return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
