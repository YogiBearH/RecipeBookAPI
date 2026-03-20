using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.GlobalExceptionHandler.Exceptions;
using System.Security.Principal;


namespace RecipeBook.Middleware
{
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> _logger,
        IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled Exception. TraceId: {TraceId}", httpContext.TraceIdentifier);

            var (status, title) = MapException(exception);

            httpContext.Response.StatusCode = status;

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Type = exception.GetType().Name,
                Detail = GetSafeErrorMessage(exception, httpContext),
                Instance = httpContext.Request.Path
            };

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            });
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                AppException appEx => ((int)appEx.StatusCode, appEx.Message),
                ArgumentNullException => (StatusCodes.Status400BadRequest, "Invalid argument provided"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument provided"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };
        }

        private static string GetProblemType(int statusCode) => statusCode switch
        {
            400 => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            401 => "https://tools.ietf.org/html/rfc9110#section-15.5.2",
            403 => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
            404 => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            409 => "https://tools.ietf.org/html/rfc9110#section-15.5.10",
            _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
        };

        private static string? GetSafeErrorMessage(Exception exception, HttpContext context)
        {
            // Only expose details in development
            var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
            if (env.IsDevelopment())
            {
                return exception.Message;
            }

            // In production, only expose messages from our own exceptions
            return exception is AppException ? exception.Message : null;
        }
    }
}
