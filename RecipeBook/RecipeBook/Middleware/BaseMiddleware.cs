namespace RecipeBook.Middleware
{
    public abstract class BaseMiddleware
    {
        protected readonly RequestDelegate _next;
        protected BaseMiddleware(RequestDelegate next) => _next = next;
        public async Task InvokeAsync(HttpContent content);
    }
}
