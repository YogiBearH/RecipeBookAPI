using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.Context;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ICtx _ctx;

        public RecipeRepository(ICtx ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await _ctx.Recipes
                .AsNoTracking()
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeSteps)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _ctx.Recipes
                .AsNoTracking()
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeSteps)
                .Where(x => x.Id == id)
                .SingleAsync();
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            await _ctx.Recipes.AddAsync(recipe);
            await _ctx.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeByIdAsync(int id, Recipe updatedRecipe)
        {
            updatedRecipe.Id = id;
            _ctx.Recipes.Update(updatedRecipe);
            await _ctx.SaveChangesAsync();
            return updatedRecipe;
        }

        public async Task DeleteRecipeById(int id)
        {
            var recipeToDelete = await _ctx.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeSteps)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (recipeToDelete != null)
            {
                _ctx.Ingredients.RemoveRange(recipeToDelete.Ingredients);
                _ctx.RecipeSteps.RemoveRange(recipeToDelete.RecipeSteps);
                _ctx.Recipes.Remove(recipeToDelete);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
