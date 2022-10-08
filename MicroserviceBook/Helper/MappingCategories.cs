using AutoMapper;
using MicroserviceBook.DTOs.Category;
using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.CategoryVM;

namespace MicroserviceBook.Helper
{
    public class MappingCategories : Profile
    {
        public MappingCategories()
        {
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<Category, GetAllCategoriesVM>();
            CreateMap<Category, GetCategoryVM>();
        }
    }
}
