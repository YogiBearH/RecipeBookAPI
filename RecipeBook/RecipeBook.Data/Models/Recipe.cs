using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Models
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }

        public ICollection<RecipeStep> RecipeSteps { get; set; }
        public ICollection<Ingredient> Ingredients { get; set;}
        public ICollection<Measurement> Measurements { get; set; }
        public ICollection<Quantity> Quantity { get; set; }
    }
}
