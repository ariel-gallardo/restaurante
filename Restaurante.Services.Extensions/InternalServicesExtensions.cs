using Microsoft.Extensions.DependencyInjection;

namespace Restaurante.Services
{
    public static class InternalServicesExtensions
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageServices, MessageServices>();
            services.AddScoped<IPasswordServices, PasswordServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ICategoriaServices, CategoriaServices>();
            services.AddScoped<IProductoServices, ProductoServices>();
            services.AddScoped<IIngredienteServices, IngredienteServices>();
            services.AddScoped<IProductoIngredienteServices, ProductoIngredienteServices>();
            services.AddScoped<IPedidoServices, PedidoServices>();
            services.AddScoped<IPositionServices, PositionServices>();
        }
    }
}
