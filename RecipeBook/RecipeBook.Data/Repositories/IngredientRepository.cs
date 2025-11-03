using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.Context;
using RecipeBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ICtx _ctx;

        public IngredientRepository(ICtx ctx)
        {
            _ctx = ctx;
        }
        public async Task DeleteIngredientById(int id)
        {
            var ingredientToDelete = await _ctx.Ingredients
                .SingleOrDefaultAsync(x => x.Id == id);
            if (ingredientToDelete != null)
            {
                _ctx.Ingredients.Remove(ingredientToDelete);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
