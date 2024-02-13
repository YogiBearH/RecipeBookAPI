using AutoMapper;
using RecipeBook.Data.Models;
using RecipeBook.DTOs.Recipes;

namespace RecipeBook.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<RecipeStep, RecipeStepDTO>().ReverseMap();
            CreateMap<Quantity, QuantityDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementDTO>().ReverseMap();
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
        }
    }
}
