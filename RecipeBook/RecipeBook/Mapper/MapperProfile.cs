using AutoMapper;
using RecipeBook.Data.Models;
using RecipeBook.DTOs.Recipes;
using System.Text.RegularExpressions;
using System.Linq;

namespace RecipeBook.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ReverseMap();
            CreateMap<RecipeStep, RecipeStepDTO>().ReverseMap();
            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src =>
                    $"{src.IngredientName} {src.Quantity} {src.MeasurementName}"))
                .ReverseMap()
                .ForPath(src => src.IngredientName, opt => opt.MapFrom(dest => ParseIngredientName(dest.Ingredient, 0)))
                .ForPath(src => src.Quantity, opt => opt.MapFrom(dest => ParseIngredientName(dest.Ingredient, 1)))
                .ForPath(src => src.MeasurementName, opt => opt.MapFrom(dest => ParseIngredientName(dest.Ingredient, 2)));
        }
        private static string ParseIngredientName(string ingredient, int index)
        {
            var parts = ingredient?.Split(' ') ?? new string[0];
            if (parts.Length >= 3)
            {
                string[] newParts = new string[3];
                var quantityPattern = @"\d*\.\d+|\d+\.\d*|\d+";
                int quantityIndex = parts
                    .Select((part, index) => new { part, index })
                    .Where(x => Regex.IsMatch(x.part, quantityPattern))
                    .Select(x => x.index)
                    .FirstOrDefault();
                newParts[0] = String.Join(" ", parts.Take(quantityIndex));
                newParts[1] = parts[quantityIndex];
                newParts[2] = String.Join(" ", parts.Skip(quantityIndex + 1));
                parts = newParts;
            }
            if (parts.Length < 3)
            {
                return string.Empty;
            }
            return index < parts.Length ? parts[index] : string.Empty;
        }
    }

}
