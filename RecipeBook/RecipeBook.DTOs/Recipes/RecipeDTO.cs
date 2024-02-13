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
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public List<RecipeStepDTO> RecipeSteps { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public List<MeasurementDTO> Measurements { get; set; }
        public List<QuantityDTO> Quantities { get; set; }
    }

    public class RecipeStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string StepDescription { get; set; }
    }

    public class IngredientDTO
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
    }

    public class MeasurementDTO
    {
        public int Id { get; set; }
        public string MeasurementName { get; set; }
    }

    public class QuantityDTO
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }
    }
}
