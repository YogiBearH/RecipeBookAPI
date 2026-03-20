using System.Net;

namespace RecipeBook.GlobalExceptionHandler.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} with identifier '{key}' was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}
