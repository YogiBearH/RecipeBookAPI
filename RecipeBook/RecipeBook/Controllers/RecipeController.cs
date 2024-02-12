using RecipeBook.Models;
using RecipeBook.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RecipeBook.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public readonly ILogger<RecipeController> Logger;
        public readonly IRecipeProvider _recipeProvider;

        public RecipeController(
            ILogger<RecipeController> logger, IRecipeProvider recipeProvider)
        {
            _logger = logger;
            _recipeProvider = recipeProvider;
        }

    }
}
