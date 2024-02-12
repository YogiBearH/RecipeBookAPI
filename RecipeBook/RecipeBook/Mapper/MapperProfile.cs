using AutoMapper;
using RecipeBook.Data.Models;
using RecipeBook.DT

namespace RecipeBook.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Recipe>
        }
    }
}
