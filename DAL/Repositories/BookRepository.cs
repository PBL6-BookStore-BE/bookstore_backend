using DAL.DataContext;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.ViewModel;

namespace DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        public readonly BookDataContext _context;

        public BookRepository(BookDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAllBooksVM>> GetAllBooks()
        {
            var list = await (from b in _context.Books
                              select new GetAllBooksVM
                              {
                                  Name = b.Name,
                                  Price = b.Price
                              }).ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<GetBookVM> GetBook(int id)
        {
            var book = await (from b in _context.Books
                              join c in _context.Categories
                              on b.IdCategory equals c.Id
                              join p in _context.Publishers
                              on b.IdPublisher equals p.Id
                              where b.Id == id
                              select new GetBookVM
                              {
                                  Name = b.Name,
                                  Category = c.Name,
                                  Pulbisher = p.Name,
                                  Authors = (from b in _context.Books
                                             join ba in _context.BookAuthors
                                             on b.Id equals ba.IdBook
                                             join a in _context.Authors
                                             on ba.IdAuthor equals a.Id
                                             where b.Id == id
                                             select new AuthorVM
                                             {
                                                 Name = a.Name
                                             }).ToList()
                              }).SingleOrDefaultAsync();
            return book;
        }
    }
}