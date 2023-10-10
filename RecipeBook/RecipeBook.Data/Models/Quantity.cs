using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Models
{
    public class Quantity
    {
        [Column("RecipeId")]
        public int RecipeId { get; set; }
        [Column("IngredientId")]
        public int IngredientId { get; set; }
        [Column("MeasurementId")]
        public int MeasurementId { get; set; }
        public float Amount { get; set; }

        private sealed class RecipeIngredientMeasurementIdEqualityComparer : IEqualityComparer<Quantity>
        {
            public bool Equals(Quantity x, Quantity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.RecipeId == y.RecipeId && x.IngredientId == y.IngredientId && x.MeasurementId == y.MeasurementId;
            }

            public int GetHashCode(Quantity obj)
            {
                return HashCode.Combine(obj.RecipeId, obj.IngredientId, obj.MeasurementId);
            }
        }
        public static IEqualityComparer<Quantity> RecipeIngredientMeasurementIdComparer { get; } = new RecipeIngredientMeasurementIdEqualityComparer();
    }
}
