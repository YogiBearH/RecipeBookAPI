using RecipeBook.Models;
using RecipeBook.Pro
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
        public readonly IRecipeP
    }
}
