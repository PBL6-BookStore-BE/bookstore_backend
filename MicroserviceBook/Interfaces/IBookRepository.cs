using MicroserviceBook.DTOs.Book;
using MicroserviceBook.ViewModels.BookVM;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetBookVM>> GetAllBooks();
        public Task<int> CreateBook(BookWithAuthorsDTO model);
        public Task<GetBookVM> GetBook (int id);
        public Task<int> UpdateBook(UpdateBookDTO model);
        public Task<int> DeleteBook(int id);

    }
}
