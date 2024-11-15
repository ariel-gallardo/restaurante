using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Models;

namespace Restaurante.API.Filters
{
    public class Status500Filter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new JsonResult(
                new ResultResponse 
                {
                    Content = !string.IsNullOrEmpty(context.Exception.StackTrace) ? context.Exception.StackTrace.Replace("\r\n", " ") : "",
                    Message = context.Exception.InnerException != null ? context.Exception.InnerException.Message.Replace("\r\n",string.Empty) : context.Exception.Message.Replace("\r\n", " "),
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
