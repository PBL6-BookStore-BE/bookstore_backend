using AutoMapper;
using DAL.DataContext;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.DTOs;
using PBL6.BookStore.Models.Entities.Book;
using PBL6.BookStore.Models.ViewModel;

namespace DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateBook(CreateBookDTO model)
        {
            var bookDto = new CreateBookDTO
            {
                Name = model.Name,
                Price = model.Price,
                Pages = model.Pages,
                PublicationDate = model.PublicationDate,
                IdCategory = model.IdCategory,
                IdPublisher = model.IdPublisher
            };
            var bookEntity = _mapper.Map<Book>(bookDto);
            _context.Books.Add(bookEntity);
            await _context.SaveChangesAsync();
            return bookEntity.Id;
        }

        public async Task<int> DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return default;
            else
            {
                book.IsDeleted = true;
                await _context.SaveChangesAsync();
                return book.Id;
            }
        }

        public async Task<IEnumerable<GetAllBooksVM>> GetAllBooks()
        {
            var list = await (from b in _context.Books
                              where b.IsDeleted == false
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

        public async Task<int> UpdateBook(UpdateBookDTO model)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == model.Id);
            if (book == null)
                return default;
            else
            {
                book.Name = model.Name;
                book.Price = model.Price;
                book.Pages = model.Pages;
                book.PublicationDate = model.PublicationDate;
                book.IdCategory = model.IdCategory;
                book.IdPublisher = model.IdPublisher;

                await _context.SaveChangesAsync();
                return book.Id;
            }
        }
    }
}