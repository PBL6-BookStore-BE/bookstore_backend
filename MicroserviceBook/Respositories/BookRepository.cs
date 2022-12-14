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
                book.Description = model.Description;
                if (model.list_img != null)
                    book.UrlImage = _picService.UploadFile(model.list_img);
                await _context.SaveChangesAsync();

                var temp_authors = await _context.BookAuthors.Where(ba => ba.IdBook == book.Id).ToListAsync();
                foreach (var author in temp_authors)
                {
                    author.IsDeleted = true;
                    author.DeletedDate = DateTime.Now;
                }
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

        public async Task<IEnumerable<GetBookVM>> Searchbook(decimal? lowprice, decimal? highprice, IList<int>? IdCategory, IList<int>? IdPublisher)
        {
            var qs = await _context.Books.Where(b => b.IsDeleted == false).ToListAsync();
            if (lowprice.HasValue)
            {
                qs = qs.Where(b => (b.Price >= lowprice)).ToList();
            }
            if (highprice.HasValue)
            {
                qs = qs.Where(b => (b.Price <= highprice)).ToList();
            }
            if (IdCategory is not null && IdCategory.Any())
            {
                qs = qs.Where(b => IdCategory.Contains(b.IdCategory)).ToList();
            }

            if (IdPublisher is not null && IdPublisher.Any())
            {
                qs = qs.Where(b => IdPublisher.Contains(b.IdPublisher)).ToList();
            }
            if (qs.Count == 0)
            {
                return new List<GetBookVM>();
            }
            else
            {
                var list = new List<GetBookVM>();
                foreach (var i in qs)
                {
                    list.Add(await _service.GetBookById(i.Id));
                }
                return list;
            }

        }

        public async Task<IEnumerable<GetBookVM>> GetBookByNameFilter(string? nameBook)
        {
            var res = String.IsNullOrEmpty(nameBook) ?
                  await _context.Books.Where(p => p.IsDeleted == false).Select(p => p.Id).ToListAsync()
                  : await _context.Books.Where(s => s.Name.ToLower().Contains(nameBook.Trim().ToLower()) && s.IsDeleted == false).Select(p => p.Id).ToListAsync();
            if (res.Count == 0) return new List<GetBookVM>();
            var books = new List<GetBookVM>();

            foreach (var i in res)
            {
                var bookVM = await _service.GetBookById(i);
                books.Add(bookVM);
            }
            return books;
        }
    }
}
