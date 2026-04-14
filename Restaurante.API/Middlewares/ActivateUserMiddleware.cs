using Restaurante.Const;
using Restaurante.Models;

namespace Restaurante.API.Middlewares
{
    public class ActivateUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ActivateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var roleClaim = user.Claims.FirstOrDefault(c => c.Type == "Rol");
                if (roleClaim != null && roleClaim.Value == Roles.Ninguno)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var response = new ResultResponse { Message = "ACCOUNT_ACTIVATE", StatusCode = StatusCodes.Status401Unauthorized, Content = new { } };
                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
            }

            await _next(context);
        }
    }
}
