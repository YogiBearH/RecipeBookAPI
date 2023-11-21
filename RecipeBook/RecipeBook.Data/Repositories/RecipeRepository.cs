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
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _ctx.Recipes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleAsync();
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            await _ctx.Recipes.AddAsync(recipe);
            await _ctx.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeByIdAsync(Recipe recipeToUpdate, int id)
        {
            recipeToUpdate.Id = id;
            _ctx.Recipes.Update(recipeToUpdate);
            await _ctx.SaveChangesAsync();
            return recipeToUpdate;
        }

        public async Task<Recipe> DeleteRecipeById(int id)
        {
            var result = await _ctx.Recipes
                .SingleOrDefaultAsync(x => x.Id == id);
            await _ctx.SaveChangesAsync();
            return result;
        }
    }
}
