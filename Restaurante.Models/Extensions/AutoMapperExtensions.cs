using Microsoft.Extensions.DependencyInjection;
using Restaurante.Models.Profiles;

namespace Restaurante.Models.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(c =>
        {
            c.AddProfile<UsuarioProfile>();
        });
    }
}
