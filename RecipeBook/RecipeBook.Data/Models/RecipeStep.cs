using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Models
{
    public class RecipeStep
    {
        [Column("RecipeId")]
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string? StepDescription { get; set; }

        private sealed class RecipeIdRecipeStepEqualityComparer : IEqualityComparer<RecipeStep>
        {
            public bool Equals(RecipeStep x, RecipeStep y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.RecipeId == y.RecipeId && x.StepNumber == y.StepNumber;
            }

            public int GetHashCode(RecipeStep obj)
            {
                return HashCode.Combine(obj.RecipeId, obj.StepNumber, obj.StepDescription);
            }
        }
        public static IEqualityComparer<RecipeStep> RecipeIdRecipeStepComparer { get; } = new RecipeIdRecipeStepEqualityComparer();
    }
}
