using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace RecipeBook.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IProblemDetailsService _problemDetails;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IProblemDetailsService problemDetails)
        {
            _logger = logger;
            _problemDetails = problemDetails;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled Exception. TraceId: {TraceId}", httpContext.TraceIdentifier);

            var (status, title) = exception switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
                ArgumentException => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Type = exception.GetType().Name,
                Detail = httpContext.RequestServices
                        .GetRequiredService<IHostEnvironment>()
                        .IsDevelopment()
                    ? exception.ToString()
                    : null,
                Instance = httpContext.Request.Path
            };

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;
            problem.Extensions["timestamp"] = DateTime.UtcNow;]

            return true;
        }
    }
}
