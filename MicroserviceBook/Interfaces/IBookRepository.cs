using MicroserviceBook.ViewModels.BookVM;

namespace MicroserviceBook.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<GetAllBooksVM>> GetAllBooks();
    }
}
