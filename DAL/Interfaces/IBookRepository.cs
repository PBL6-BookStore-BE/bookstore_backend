using PBL6.BookStore.Models.Entities.Book;

namespace DAL.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
    }
}