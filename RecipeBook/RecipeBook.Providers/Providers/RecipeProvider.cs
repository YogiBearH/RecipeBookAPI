using Microsoft.Extensions.Logging;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Models;
using RecipeBook.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Providers.Providers
{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly ILogger<RecipeProvider> _logger;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeProvider(IRecipeRepository recipeRepository, ILogger<RecipeProvider> logger)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            IEnumerable<Recipe> recipes = await _recipeRepository.GetRecipesAsync();
            return recipes;
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            Recipe recipe = await _recipeRepository.GetRecipeById(id);
            return recipe;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            Recipe newRecipe = await _recipeRepository.CreateRecipeAsync(recipe);
            return newRecipe;
        }

        public async Task<Recipe> UpdateRecipeByIdAsync(Recipe recipeToUpdate, int id)
        {
            Recipe newInfo;
            Recipe oldInfo = await _recipeRepository.GetRecipeById(id);

            if (oldInfo == null || oldInfo == default) 
            {
                throw new Exception();
            }

            newInfo.Id = id;
            newInfo.DateCreated = oldInfo.DateCreated;
            newInfo.DateModified = DateTime.Now;

            newInfo = await _recipeRepository.UpdateRecipeByIdAsync(recipeToUpdate, id);
            return newInfo;
        }
    }
}
