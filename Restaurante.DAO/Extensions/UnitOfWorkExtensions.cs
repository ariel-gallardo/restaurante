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
             .AddScoped<IRepository<Usuario>, BigIntRepository<Usuario>>();

            services =
                services.AddScoped<IUsuarioRepository, UsuarioRepository>();

             
             return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
