using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Migrations;
using Restaurante.Services;

namespace Restaurante.API
{
    public static class SqliteExtensions
    {
        public static IServiceCollection AddSqliteCFG(this IServiceCollection services, IConfiguration cfg)
        {
            bool useSqlite = false;
            Boolean.TryParse(cfg["UseSqlite"], out useSqlite);
            AppSettings.UseSqlite = useSqlite;

            if (AppSettings.UseSqlite)
            {

                var dbDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
                if (Directory.Exists(dbDirectory))
                {
                    Directory.Delete(dbDirectory, true);
                    Directory.CreateDirectory(dbDirectory);
                }
                services.AddDbContext<RestauranteContext>(options =>
                {
                    options.UseSqlite($"Data Source={Path.Combine(dbDirectory,"Restaurante.sqlite")}");
                });
            }
            return services;
        }
    }
}
