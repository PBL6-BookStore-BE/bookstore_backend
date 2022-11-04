using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Book;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;
using MicroserviceBook.Service;

namespace MicroserviceBook.Respositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        private readonly IGetBookService _service;
        private readonly IPictureService _picService;

        public BookRepository(BookDataContext context, IMapper mapper, IGetBookService service,IPictureService pictureService)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _picService = pictureService;

        }
        public async Task<IEnumerable<GetBookVM>> GetAllBooks()
        {
            var list = await _context.Books.Where(b => b.IsDeleted == false).ToListAsync();
            var result = new List<GetBookVM>();

            foreach (var i in list)
            {
                var bookVM = await _service.GetBookById(i.Id);
                result.Add(bookVM);
            }


            return result;

        }
        public async Task<int> CreateBook(BookWithAuthorsDTO model)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                var book = new Book()
                {
                    Name = model.Name,
                    Pages = model.Pages,
                    Rating = 0,
                    Price = model.Price,
                    PublicationDate = model.PublicationDate,
                    IdCategory = model.IdCategory,
                    IdPublisher = model.IdPublisher,
                    Description = model.Description,
                    UrlImage = _picService.UploadFile(model.list_img)

            };
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
                    await _context.SaveChangesAsync();
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
        }

        public async Task<GetBookVM> GetBook(int id)
        {
          var result =  await _service.GetBookById(id);
            return result;
             
        }

        public async Task<int> UpdateBook(UpdateBookDTO model)
        {
            var book = await _context.Books.Where(b => b.Id == model.Id).SingleOrDefaultAsync();
            if (book == null)
            {
                return 0;
            }
            else
            {
                book.Name = model.Name;
                book.Pages = model.Pages;
                book.Price = model.Price;
                book.PublicationDate = model.PublicationDate;
                book.IdCategory = model.IdCategory;
                book.IdPublisher = model.IdPublisher;
                await _context.SaveChangesAsync();
                return book.Id;
            }

        }

        public async Task<int> DeleteBook(int id)
        {
            var book = await _context.Books.Where(b => b.Id == id).SingleOrDefaultAsync();

            if (book == null)
            {
                return default;
            }
            else
            {
                book.IsDeleted = true;
                book.DeletedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return book.Id;
            }
        }

        public async Task<IEnumerable<GetBookVM>> Top10ByRating()
        {
            var list = await _context.Books.OrderByDescending(b => b.Rating).Take(10).ToListAsync();
            var result = new List<GetBookVM>();

            foreach (var i in list)
            {
                var bookVM = await _service.GetBookById(i.Id);
            
                result.Add(bookVM);
            }
            return result;

        }

        public async Task<IEnumerable<GetBookVM>> GetBookByPriceFilter(BookWithPrice model)
        {
            var result = await _context.Books.Where(b => (b.Price >= model.lowest && b.Price <= model.highest)).OrderBy(b => b.Price).ToListAsync();
            if (result.Count == 0)
            {
                return default;
            }
            else
            {
                var list = new List<GetBookVM>();
                foreach ( var i in result){
                    list.Add(await _service.GetBookById(i.Id));
                }
                return list;
            }
        }
    }
}
