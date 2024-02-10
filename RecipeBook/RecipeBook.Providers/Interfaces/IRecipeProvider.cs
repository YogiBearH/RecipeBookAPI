﻿using RecipeBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Providers.Interfaces
{
    public interface IRecipeProvider
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeById(int id);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeByIdAsync(Recipe recipeToUpdate, int id);
        Task<Recipe> DeleteRecipeById(int id);
    }
}