
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex,logger,env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";

            var response = env.IsDevelopment()
                ? new ApiException(statusCode, exception.Message, exception.StackTrace?.ToString())
                : new ApiException(statusCode, exception.Message, message);

            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsJsonAsync(json);
        }
    }
    
    
}