using MicroserviceBook.DTOs.Book;
using MicroserviceBook.ViewModels.BookVM;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetBookVM>> GetAllBooks();
        public Task<int> CreateBook(BookWithAuthorsDTO model);
        public Task<GetBookVM> GetBook(int id);
        public Task<int> UpdateBook(UpdateBookDTO model);
        public Task<int> DeleteBook(int id);
        public Task<IEnumerable<GetBookVM>> Top10ByRating();

        public Task<IEnumerable<GetBookVM>> Searchbook(decimal? lowprice, decimal? highprice, IList<int>? IdCategory, IList<int>? IdPublisher);
        public Task<IEnumerable<GetBookVM>> GetBookByNameFilter(string? nameBook);

    }
}
