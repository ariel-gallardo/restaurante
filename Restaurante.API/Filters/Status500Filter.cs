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
                    Content = context.Exception.StackTrace,
                    Message = context.Exception.Message,
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
