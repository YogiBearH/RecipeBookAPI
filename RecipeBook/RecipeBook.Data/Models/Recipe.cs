using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Models
{
    public class Recipe : BaseEntity
    {
        public string ?RecipeName { get; set; }
        public string ?Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public List<Ingredient>? Ingredients { get; set;}
        public List<RecipeStep>? RecipeSteps { get; set; }

        private sealed class ProductEqualityComparer : IEqualityComparer<Recipe>
        {
            public bool Equals(Recipe x, Recipe y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.RecipeName == y.RecipeName && x.Description == y.Description && x.PrepTime == y.PrepTime && x.CookTime == y.CookTime;
            }

            public int GetHashCode(Recipe obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.RecipeName);
                hashCode.Add(obj.Description);
                hashCode.Add(obj.PrepTime);
                hashCode.Add(obj.CookTime);
                hashCode.Add(obj.Ingredients);
                hashCode.Add(obj.RecipeSteps);
                return hashCode.ToHashCode();
            }
        }
    }
}
