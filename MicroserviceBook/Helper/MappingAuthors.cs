using AutoMapper;
using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Entities;

namespace MicroserviceBook.Helper
{
    public class MappingAuthors : Profile
    {
        public MappingAuthors()
        {

            CreateMap<CreateAuthorDTO, Author>();
        }
    }
}
