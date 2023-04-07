using Microsoft.AspNetCore.Mvc;

namespace myRecipeBookAPI.Controllers
{
    public class RecipeController : Controller
    {
        public string Index()
        {
            return "This is the page for the Recipes";
        }
    }
}
