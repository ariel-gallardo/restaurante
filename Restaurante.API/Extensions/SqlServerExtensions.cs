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
                services.AddDbContext<RestauranteContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(AppSettings.MSSQLConnectionString);
                    var env = serviceProvider.GetService<IWebHostEnvironment>();
                    if (env != null && env.IsDevelopment())
                    {
                        options.EnableSensitiveDataLogging();
                    }
                });
            }
            return services;
        }
    }
}
