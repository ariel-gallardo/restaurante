using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.OpenApi.Models;
using Restaurante.API.Extensions;
using Restaurante.API.Filters;
using Restaurante.API.Middlewares;
using Restaurante.DAO.Extensions;
using Restaurante.Hubs;
using Restaurante.Infraestructure;
using Restaurante.Models.Extensions;
using Restaurante.Services;
using Restaurante.Services.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Restaurante.API
{
    public class Startup
    {
        private readonly IConfiguration _cfg;
        public Startup(IConfiguration cfg)
        {
            _cfg = cfg;
            _cfg.AddAppSettingsCustomCFG();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.WithOrigins(AppSettings.ClientURL)
                           .AllowCredentials()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            
            // Agrega los controladores
            services.AddControllers(o =>
            {
                o.Filters.Add<DeactivateMethodFilter>();
                o.Filters.Add<Status500Filter>();
                o.Filters.Add<ValidationFilter>();
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services
                .AddSignalR()
                .AddJsonProtocol();
            services.AddHttpContextAccessor();
            services.AddCustomAutoMapper();
            
            // Configura Swagger/OpenAPI
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Restaurante API",
                    Description = ".NetCore Restaurante API",
                    Contact = new OpenApiContact
                    {
                        Name = "LinkedIn",
                        Url = new Uri("https://www.linkedin.com/in/ariel-alejandro-gallardo-dev/")
                    }
                });

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor ingrese el token en formato 'Bearer {token}'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                services.AddAuthorization();
                o.ExampleFilters();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var ctrlOutput = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                o.IncludeXmlComments(ctrlOutput, true);
                o.EnableAnnotations();
            });
            services.AddInternalServices();
            services
                .AddSqliteCFG(_cfg)
                .AddSqlServerCFG(_cfg)
                .AddUnitOfWork()
                .AddCustomJWT();
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configura el pipeline de solicitudes HTTP
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.CheckDatabase();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseMiddleware<ActivateUserMiddleware>();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StatusHub>("/ws");
                endpoints.MapHub<PedidoHub>("/ws/pedidos");
                endpoints.MapHub<MessagesHub>("/ws/messages");
            });
        }
    }
}
