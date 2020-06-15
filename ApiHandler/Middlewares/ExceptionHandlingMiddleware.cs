using ApiHandler.Handler;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ApiHandler.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                //Aqui se puede creat un log para la exception.
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new ErrorResponse();

            if (exception is HttpException httpException)
            {
                errorResponse.StatusCode = httpException.StatusCode;
                errorResponse.Message = httpException.Message;
            }

            context.Response.StatusCode = (int)errorResponse.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorResponse.ToJsonString());
        }
    }
}
