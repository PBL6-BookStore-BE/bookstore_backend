using AutoMapper;
using MicroserviceBook.DTOs.Book;
using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.BookVM;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BookWithAuthorsDTO, Book>();
            CreateMap<Book, GetAllBooksVM>();
        }
    }
}