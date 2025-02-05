using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Models;
using System.Linq;
using System.Reflection;

namespace Restaurante.API.Filters
{
    public class DeactivateMethodFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor;

            if (actionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controllerActionDescriptor)
            {
                var metodo = controllerActionDescriptor.MethodInfo;
                var atributo = metodo.GetCustomAttributes(typeof(DeactivateMethodAttribute), true)
                                     .FirstOrDefault() as DeactivateMethodAttribute;

                if (atributo != null && atributo.Desactivado)
                {
                    context.Result = new JsonResult(new ResultResponse { Message = $@"ROUTE_DISABLED ""{context.HttpContext.Request.Path}""", StatusCode = StatusCodes.Status403Forbidden });
                    await Task.CompletedTask;
                }
            }
            
        }
    }
}
