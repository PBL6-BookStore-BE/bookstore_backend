using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Book;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.BookVM;

using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
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
        public async Task<IEnumerable<GetAllBooksVM>> GetAllBooks()
        {
            var list = await _context.Books.Where(b => b.IsDeleted == false).ToListAsync();
            var booksVM = _mapper.Map<IEnumerable<GetAllBooksVM>>(list);
            return booksVM;

    
        }

        public async Task<int> CreateBook(BookWithAuthorsDTO model)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                var book = _mapper.Map<Book>(model);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                foreach (var id in model.IdAuthors)
                {
                    var _book_author = new BookAuthor()
                    {
                        IdBook = book.Id,
                        IdAuthor = id
                    };
                    _context.BookAuthors.Add(_book_author);
                await  _context.SaveChangesAsync();
                }
                await dbContextTransaction.CommitAsync();
                await dbContextTransaction.DisposeAsync();
                return book.Id;
            }

            catch (Exception)
            {

                await dbContextTransaction.RollbackAsync();
                await dbContextTransaction.DisposeAsync();
                throw;
            }
            //var bookDto = new CreateBookDTO
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Pages = model.Pages,
            //    PublicationDate = model.PublicationDate,
            //    IdCategory = model.IdCategory,
            //    IdPublisher = model.IdPublisher
            //};
            //var bookEntity = _mapper.Map<Book>(bookDto);
            //_context.Books.Add(bookEntity);
            //await _context.SaveChangesAsync();
            //return bookEntity.Id;
        }
    }
}
