using PBL6.BookStore.Models.ViewModel;

namespace DAL.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetAllBooksVM>> GetAllBooks();

        public Task<GetBookVM> GetBook(int id);
    }
}