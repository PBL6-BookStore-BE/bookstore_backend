using AutoMapper;
using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.AuthorVM;

namespace MicroserviceBook.Helper
{
    public class MappingAuthors : Profile
    {
        public MappingAuthors()
        {

            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<Author, GetAllAuthorsVM>();
        }
    }
}
