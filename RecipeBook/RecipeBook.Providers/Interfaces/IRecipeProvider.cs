using RecipeBook.Data.Models;
using RecipeBook.DTOs.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Providers.Interfaces
{
    public interface IRecipeProvider
    {
        Task<IEnumerable<RecipeDTO>> GetRecipesAsync();
        Task<RecipeDTO> GetRecipeById(int id);
        Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipe);
        Task<RecipeDTO> UpdateRecipeByIdAsync(int id, RecipeDTO updatedRecipe);
        Task DeleteRecipeById(int id);
    }
}
