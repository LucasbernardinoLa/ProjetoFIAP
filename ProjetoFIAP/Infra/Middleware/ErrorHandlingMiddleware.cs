using System.Net;
using System.Text.Json;
using ProjetoFIAP.Api.Domain.Exceptions;

namespace ProjetoFIAP.Api.Infra.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new
            {
                Type = exception.GetType().Name,
                exception.Message,
                exception.StackTrace
            };

            var statusCode = exception switch
            {
                DomainValidationException => HttpStatusCode.BadRequest,
                KeyNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Error = response.Type,
                response.Message,
#if DEBUG
                response.StackTrace
#endif
            });

            await context.Response.WriteAsync(result);
        }
    }

    // Extensão para facilitar o registro do middleware
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyErrorHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}