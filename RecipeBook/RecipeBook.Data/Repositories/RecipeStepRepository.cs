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
    public class RecipeStepRepository : IRecipeStepRepository
    {
        private readonly ICtx _ctx;

        public RecipeStepRepository(ICtx ctx) 
        {
            _ctx = ctx;
        }
        public async Task DeleteRecipeStepById(int id) 
        {
            var stepToDelete = await _ctx.RecipeSteps
                .SingleOrDefaultAsync(x => x.Id == id);
            if (stepToDelete != null)
            {
                _ctx.RecipeSteps.Remove(stepToDelete);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
