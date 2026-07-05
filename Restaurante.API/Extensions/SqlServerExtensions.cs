using Microsoft.EntityFrameworkCore;
using Restaurante.Infraestructure;
using Restaurante.Migrations;
using Restaurante.Services;
using System.IO;

namespace Restaurante.API
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddSqlServerCFG(this IServiceCollection services, IConfiguration cfg)
        {
            if (!AppSettings.UseSqlite)
            {
                AppSettings.MSSQLConnectionString = cfg["MSSQL:ConnectionString"];
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                services.AddDbContext<RestauranteContext>(options =>
                {
                    options.UseSqlServer(AppSettings.MSSQLConnectionString);
                    if (environment == "Development")
                        options.EnableSensitiveDataLogging();
                });
            }
            return services;
        }
    }
}
