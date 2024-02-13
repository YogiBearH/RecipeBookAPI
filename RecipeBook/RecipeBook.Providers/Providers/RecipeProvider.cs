using Microsoft.Extensions.Logging;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Models;
using RecipeBook.Providers.Interfaces;
using AutoMapper;
using RecipeBook.DTOs.Recipes;
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
        private readonly IMapper _mapper;

        public RecipeProvider(IRecipeRepository recipeRepository, ILogger<RecipeProvider> logger, IMapper mapper)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
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
            Recipe oldRecipe = await _recipeRepository.GetRecipeById(id);

            if (oldRecipe == null) 
            {
                throw new Exception($"Recipe with id {id} not found");
            }

            var updatedModel = _mapper.Map<Recipe>(updatedRecipe);
            updatedModel.Id = id;

            updatedModel = await _recipeRepository.UpdateRecipeByIdAsync(id, updatedModel);
            return _mapper.Map<RecipeDTO>(updatedRecipe);
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
