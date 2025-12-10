using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security;

namespace TransportCompanies.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception e)
            {
                _logger.LogError(e, "Erro não esperado");
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                FormatException => StatusCodes.Status400BadRequest,
                ArgumentNullException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                DbUpdateException => StatusCodes.Status500InternalServerError,     
                DuplicateNameException => StatusCodes.Status409Conflict,
                SecurityException => StatusCodes.Status403Forbidden,
                TimeoutException => StatusCodes.Status504GatewayTimeout,
                SqlException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            return context.Response.WriteAsJsonAsync(new
            {
                error = exception.Message,
                StatusCodes = context.Response.StatusCode
            });
        }
    }
}
