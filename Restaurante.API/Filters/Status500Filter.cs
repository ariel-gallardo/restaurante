using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Models;

namespace Restaurante.API.Filters
{
    public class Status500Filter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public Status500Filter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new ResultResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "INTERNAL_SERVER_ERROR"
            };

            if (_env.IsDevelopment())
            {
                response.Content = !string.IsNullOrEmpty(context.Exception.StackTrace) ? context.Exception.StackTrace.Replace("\r\n", " ") : "";
                response.Message = context.Exception.InnerException != null ? context.Exception.InnerException.Message.Replace("\r\n", string.Empty) : context.Exception.Message.Replace("\r\n", " ");
            }

            context.Result = new JsonResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}
