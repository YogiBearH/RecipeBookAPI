using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.DTOs.Recipes
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string RecipeName { get; set; }

        public string Description { get; set; }

        public int PrepTime { get; set; }
        public int CookTime { get; set; }

        public List<string>? RecipeSteps { get; set; }
        
        public List<string>? Ingredients { get; set; }
        
        public List<string>? Measurements { get; set; }
        
        public List<string>? Quantity { get; set; }

    }
}
