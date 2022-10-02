using DAL.DataContext;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Book;

namespace DAL.Repositories
{
    public class BookRepository: IBookRepository
    {
        public readonly BookDataContext _context;

        public BookRepository(BookDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return (await _context.Books.ToListAsync());
        }
    }
}