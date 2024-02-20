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
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
        }
    }
}
