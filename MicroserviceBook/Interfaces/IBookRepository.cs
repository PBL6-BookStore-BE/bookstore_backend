using MicroserviceBook.DTOs.Book;
using MicroserviceBook.ViewModels.BookVM;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetAllBooksVM>> GetAllBooks();
        public Task<int> CreateBook(BookWithAuthorsDTO model);
    }
}
