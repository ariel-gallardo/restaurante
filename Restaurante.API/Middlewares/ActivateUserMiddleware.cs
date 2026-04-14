using Restaurante.Const;
using Restaurante.Infraestructure;
using Restaurante.Migrations;
using Restaurante.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurante.API.Middlewares
{
    public class ActivateUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ActivateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RestauranteContext dbContext)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    var key = Encoding.UTF8.GetBytes(AppSettings.JWTSecretKey);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = validatedToken as JwtSecurityToken;
                    if (jwtToken != null)
                    {
                        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Rol");
                        if (roleClaim != null && roleClaim.Value == Roles.Ninguno)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var response = new ResultResponse { Message = "ACCOUNT_ACTIVATE", StatusCode = StatusCodes.Status401Unauthorized, Content = new { } };
                            await context.Response.WriteAsJsonAsync(response);
                            return;
                        }
                    }
                }
                catch (SecurityTokenException)
                {
                    // Token validation failed — let the [Authorize] middleware handle it downstream
                }
            }

            await _next(context);
        }
    }

}
