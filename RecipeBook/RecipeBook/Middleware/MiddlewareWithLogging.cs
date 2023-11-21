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
        public async override Task InvokeAsync(HttpContent content);
    }
}
