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
                .ForMember(dest => dest.RecipeSteps, opt => opt.MapFrom(src => src.RecipeSteps))
                .ReverseMap();
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
            CreateMap<RecipeStep, RecipeStepDTO>().ReverseMap();
        }
    }

}
