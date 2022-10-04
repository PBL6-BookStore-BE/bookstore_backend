using PBL6.BookStore.Models.DTOs.Book.BookDTO;
using PBL6.BookStore.Models.ViewModel.Book.BookVM;

namespace DAL.Interfaces.IBookRepositories
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetAllBooksVM>> GetAllBooks();

        public Task<GetBookVM> GetBook(int id);

        public Task<int> CreateBook(CreateBookDTO model);

        public Task<int> UpdateBook(UpdateBookDTO model);
        public Task<int> DeleteBook(int id);
    }
}