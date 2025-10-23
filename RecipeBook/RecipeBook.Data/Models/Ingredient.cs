using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Models
{
    public class Ingredient : BaseEntity
    {
        public string IngredientName { get; set; }
        public float Quantity { get; set; }
        public string MeasurementName { get; set; }

        public static IEqualityComparer<Ingredient> IngredientComparer { get; } = new IngredientEqualityComparer();

        private sealed class IngredientEqualityComparer : IEqualityComparer<Ingredient>
        {
            public bool Equals(Ingredient x, Ingredient y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x == null || y == null) return false;
                return x.IngredientName == y.IngredientName && x.Quantity == y.Quantity && x.MeasurementName == y.MeasurementName;
            }

            public int GetHashCode(Ingredient obj) 
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.IngredientName.ToLowerInvariant());
                hashCode.Add(obj.Quantity);
                hashCode.Add(obj.MeasurementName.ToLowerInvariant());
                return hashCode.ToHashCode();
            }
        }
    }
}
