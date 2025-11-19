using Microsoft.Extensions.Logging;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Models;
using RecipeBook.Providers.Interfaces;
using AutoMapper;
using RecipeBook.DTOs.Recipes;

namespace RecipeBook.Providers.Providers
{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly ILogger<RecipeProvider> _logger;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeStepRepository _recipeStepRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeProvider
            (IRecipeRepository recipeRepository,
            IRecipeStepRepository recipeStepRepository, 
            IIngredientRepository ingredientRepository,
            ILogger<RecipeProvider> logger,
            IMapper mapper)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
            _recipeStepRepository = recipeStepRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeDTO>> GetRecipesAsync()
        {
            IEnumerable<Recipe> recipes = await _recipeRepository.GetRecipesAsync();
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
        }

        public async Task<RecipeDTO> GetRecipeById(int id)
        {
            var recipe = await _recipeRepository.GetRecipeById(id);
            return _mapper.Map<RecipeDTO>(recipe);
        }

        public async Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipeDTO)
        {
            Recipe recipe = _mapper.Map<Recipe>(recipeDTO);
            var newRecipe = await _recipeRepository.CreateRecipeAsync(recipe);
            return _mapper.Map<RecipeDTO>(newRecipe);
        }

        public async Task<RecipeDTO> UpdateRecipeByIdAsync(int id, RecipeDTO updatedRecipe)
        {
            var oldRecipe = await _recipeRepository.GetRecipeById(id);

            if (oldRecipe == null)
            {
                throw new Exception($"Recipe with id {id} not found");
            }

            var updatedModel = _mapper.Map<Recipe>(updatedRecipe);

            //Checking number of Recipe Steps of each
            int oldRecipeStepCount = oldRecipe.RecipeSteps.Count;
            int updatedModelCount = updatedRecipe.RecipeSteps.Count;

            /* Seeing if the number of steps matches, if so, grab old id for new id. If old 
             * steps are higher, delete extras. If old steps lower, leave alone for it to 
             * create its own id */
            if (oldRecipeStepCount == updatedModelCount)
            {
                for (int i = 0; i < updatedModelCount; i++)
                {
                    updatedModel.RecipeSteps[i].Id = oldRecipe.RecipeSteps[i].Id;
                }
            }
            else if (oldRecipeStepCount > updatedModelCount)
            {
                //Number of steps to update is the number of updated steps since it is less than old steps
                for (int i = 0; i < updatedModelCount; i++)
                {
                    updatedModel.RecipeSteps[i].Id = oldRecipe.RecipeSteps[i].Id;
                }
                //Delete extra steps from old recipe. Starts from where updated steps end to old steps end
                for (int j = updatedModelCount; j < oldRecipeStepCount; j++)
                {
                    await _recipeStepRepository.DeleteRecipeStepById(oldRecipe.RecipeSteps[j].Id);
                }
            }
            else
            {
                //Old steps are less than updated steps, so only update the old steps. Let new steps create new ids
                for (int i = 0; i < oldRecipeStepCount; i++)
                {
                    updatedModel.RecipeSteps[i].Id = oldRecipe.RecipeSteps[i].Id;
                }
            }

            int oldIngredientCount = oldRecipe.Ingredients.Count;
            int updatedIngredientCount = updatedRecipe.Ingredients.Count;

            if (oldIngredientCount == updatedIngredientCount)
            {
                for (int i = 0; i < updatedIngredientCount; i++)
                {
                    updatedModel.Ingredients[i].Id = oldRecipe.Ingredients[i].Id;
                }
            }
            else if (oldIngredientCount > updatedIngredientCount)
            {
                //Number of ingredients to update is the number of updated ingredients since it is less than old ingredients
                for (int i = 0; i < updatedIngredientCount; i++)
                {
                    updatedModel.Ingredients[i].Id = oldRecipe.Ingredients[i].Id;
                }
                //Delete extra ingredients from old recipe. Starts from where updated ingredients end to old ingredients end
                for (int j = updatedIngredientCount; j < oldIngredientCount; j++)
                {
                    await _ingredientRepository.DeleteIngredientById(oldRecipe.Ingredients[j].Id);
                }
            }
            else
            {
                //Old ingredients are less than updated ingredients, so only update the old ingredients. Let new ingredients create new ids
                for (int i = 0; i < oldIngredientCount; i++)
                {
                    updatedModel.Ingredients[i].Id = oldRecipe.Ingredients[i].Id;
                }
            }

            await _recipeRepository.UpdateRecipeByIdAsync(id, updatedModel);

            return updatedRecipe;
        }

        public async Task DeleteRecipeById(int id)
        {
            RecipeDTO recipe = await GetRecipeById(id);
            
            if (recipe != null) 
            {
                await _recipeRepository.DeleteRecipeById(id);
            }
            else
            {
                throw new Exception($"Recipe with id {id} not found");
            }
        }
    }
}
