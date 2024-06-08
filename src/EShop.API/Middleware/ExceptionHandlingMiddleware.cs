using EShop.API.Exception;
using System.Net;

namespace EShop.API.Middleware
{
    public class ExceptionHandlingMiddleWare(RequestDelegate next, ILogger<ExceptionHandlingMiddleWare> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            logger.LogError(exception, "An unexpected error occurred.");

            //More log stuff        

            ExceptionResponse response = exception switch
            {
                ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, exception.Message)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
