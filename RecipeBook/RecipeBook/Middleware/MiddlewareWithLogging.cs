using log4net;

namespace RecipeBook.Middleware
{
    public abstract class MiddlewareWithLogging : BaseMiddleware
    {
        protected readonly ILog _log;
        protected MiddlewareWithLogging(RequestDelegate next, ILog log) : base(next)
        {
            _log = log;
        }
        public abstract override Task InvokeAsync(HttpContext content);
    }
}
