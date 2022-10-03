using AutoMapper;
using PBL6.BookStore.Models.DTOs;
using PBL6.BookStore.Models.Entities.Book;

namespace DAL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateBookDTO, Book>();
        }
    }
}