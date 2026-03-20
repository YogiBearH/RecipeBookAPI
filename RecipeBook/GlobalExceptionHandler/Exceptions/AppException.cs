using System.Net;

namespace RecipeBook.GlobalExceptionHandler.Exceptions
{
    public abstract class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AppException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
