using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations;

namespace Restaurante.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IApplicationBuilder CheckDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<RestauranteContext>();
                ctx.Database.Migrate();
            }

            return app;
        }
    }
}
