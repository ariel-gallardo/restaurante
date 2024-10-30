using Restaurante.Services;

namespace Restaurante.API
{
    public static class InternalServicesExtensions
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordServices, PasswordServices>();
            services.AddTransient<IUserServices, UserServices>();
        }
    }
}
