using Microsoft.Extensions.DependencyInjection;

namespace Restaurante.Services
{
    public static class InternalServicesExtensions
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordServices, PasswordServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IProductoServices, ProductoServices>();
            services.AddTransient<IIngredienteServices, IngredienteServices>();
            services.AddTransient<IProductoIngredienteServices, ProductoIngredienteServices>();
        }
    }
}
