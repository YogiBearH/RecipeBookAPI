using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.DTOs.Recipes
{
    public class RecipeDTO
    {
        public int Id { get; set; }

        [Required]
        public string RecipeName { get; set; }
        [Required]
        public string Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public List<IngredientDTO>? Ingredients { get; set; }
        public List<RecipeStepDTO>? RecipeSteps { get; set; }
    }

    public class RecipeStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? StepDescription { get; set; }
    }

    public class IngredientDTO
    {
        public int Id { get; set; }
        [Required]
        public string IngredientName { get; set; }
        public float Quantity { get; set; }
        public string MeasurementName { get; set; }
    }
}