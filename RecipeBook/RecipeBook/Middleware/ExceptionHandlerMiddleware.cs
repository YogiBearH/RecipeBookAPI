using log4net;

namespace RecipeBook.Middleware
{
    public class ExceptionHandlerMiddleware : MiddlewareWithLogging
    {

        public ExceptionHandlerMiddleware(RequestDelegate next, ILog log) :
            base(next, log)
        {

        }

        public override async Task InvokeaAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }
    }
}
