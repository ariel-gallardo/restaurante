using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Models;

namespace Restaurante.API.Filters
{
    public class Status500Filter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var env = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
            var isDevelopment = env != null && env.EnvironmentName == "Development";

            context.Result = new JsonResult(
                new ResultResponse 
                {
                    Content = isDevelopment
                        ? (!string.IsNullOrEmpty(context.Exception.StackTrace) ? context.Exception.StackTrace.Replace("\r\n", " ") : "")
                        : null,
                    Message = isDevelopment
                        ? (context.Exception.InnerException != null ? context.Exception.InnerException.Message.Replace("\r\n",string.Empty) : context.Exception.Message.Replace("\r\n", " "))
                        : "INTERNAL_SERVER_ERROR",
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            )
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}
