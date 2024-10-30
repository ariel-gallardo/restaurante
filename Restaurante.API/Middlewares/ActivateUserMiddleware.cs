using Restaurante.Const;
using Restaurante.Migrations;
using Restaurante.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {

                    var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
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

            await _next(context);
        }
    }

}
