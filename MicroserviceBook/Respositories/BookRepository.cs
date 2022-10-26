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
        public async Task<IEnumerable<GetBookVM>> GetAllBooks()
        {
            var list = await _context.Books.Where(b => b.IsDeleted == false).ToListAsync();
            var result = new List<GetBookVM>();

            foreach (var i in list)
            {
                var bookVM = new GetBookVM();
                bookVM.PublicationDate = i.PublicationDate;
                bookVM.Name = i.Name;
                bookVM.Pages = i.Pages;
                bookVM.Rating = i.Rating;
                bookVM.Price = i.Price;
                bookVM.UrlFolder = i.UrlImage;
                bookVM.Urls = GetUrls(i.UrlImage);
                bookVM.CategoryName = _context.Categories.Where(c => c.Id == i.IdCategory).Select(c => c.Name).Single();
                bookVM.PublisherName = _context.Publishers.Where(p => p.Id == i.IdPublisher).Select(p => p.Name).Single();
                bookVM.Authors =
                    (from ba in _context.BookAuthors
                     join a in _context.Authors on ba.IdAuthor equals a.Id
                     where ba.IdBook == i.Id
                     select a.Name).ToList();
                result.Add(bookVM);
            }


            foreach (var i in list)
            {
                var bookVM = new GetBookVM();
                bookVM.PublicationDate = i.PublicationDate;
                bookVM.Name = i.Name;
                bookVM.Pages = i.Pages;
                bookVM.Rating = i.Rating;
                bookVM.Price = i.Price;
                bookVM.UrlFolder = i.UrlImage;
                bookVM.Urls = GetUrls(i.UrlImage);
                bookVM.CategoryName = _context.Categories.Where(c => c.Id == i.IdCategory).Select(c => c.Name).Single();
                bookVM.PublisherName = _context.Publishers.Where(p => p.Id == i.IdPublisher).Select(p => p.Name).Single();
                bookVM.Authors =
                    (from ba in _context.BookAuthors
                     join a in _context.Authors on ba.IdAuthor equals a.Id
                     where ba.IdBook == i.Id
                     select a.Name).ToList();
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
                    UrlImage = UploadFile(model.list_img)

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

        public string UploadFile(List<IFormFile> list_img)
        {
            Account account = new Account(
                    "dgs9vyh4n",
                    "759658434427383",
                    "oobrP1pOzKOb9q7E9vB_jBQqQHY");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            var guiID = Guid.NewGuid();
            string rootFolder = "book/" + guiID + "/";
            string temp = "";
            foreach (var img in list_img)
            {
                temp = rootFolder + Path.GetFileNameWithoutExtension(img.FileName);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(img.FileName, img.OpenReadStream()),
                    PublicId = temp
                };
                var uploadResult = cloudinary.Upload(uploadParams);
            }
            return rootFolder;
        }

        public IEnumerable<string> GetUrls(string url_folder)
        {
            var list_url = new List<string>();
            Account account = new Account(
                  "dgs9vyh4n",
                  "759658434427383",
                  "oobrP1pOzKOb9q7E9vB_jBQqQHY");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            //var result = cloudinary.ListResources();
            SearchResult result = cloudinary.Search()
                .Expression(url_folder)
                .WithField("context")
                .WithField("tags")
                .MaxResults(10)
                .Execute();

            var k = result.Resources;

            foreach (var i in k)
            {
                list_url.Add(i.Url.ToString());
            }
            return list_url;

        }

        public async Task<IEnumerable<GetBookVM>> Top10ByRating()
        {
            var list = await _context.Books.OrderByDescending(b => b.Rating).Take(10).ToListAsync();
            var result = new List<GetBookVM>();

            foreach (var i in list)
            {
                var bookVM = new GetBookVM();
                bookVM.PublicationDate = i.PublicationDate;
                bookVM.Name = i.Name;
                bookVM.Pages = i.Pages;
                bookVM.Rating = i.Rating;
                bookVM.Price = i.Price;
                bookVM.UrlFolder = i.UrlImage;
                bookVM.Urls = GetUrls(i.UrlImage);
                bookVM.CategoryName = _context.Categories.Where(c => c.Id == i.IdCategory).Select(c => c.Name).Single();
                bookVM.PublisherName = _context.Publishers.Where(p => p.Id == i.IdPublisher).Select(p => p.Name).Single();
                bookVM.Authors =
                    (from ba in _context.BookAuthors
                     join a in _context.Authors on ba.IdAuthor equals a.Id
                     where ba.IdBook == i.Id
                     select a.Name).ToList();
                result.Add(bookVM);
            }
            return result;

        }

        public async Task<GetBookVM> GetBook(int id)
        {
            var book = await (from b in _context.Books
                              join
                                   c in _context.Categories
                                   on b.IdCategory equals c.Id
                              join p in _context.Publishers
                              on b.IdPublisher equals p.Id
                              where b.Id == id
                              select new GetBookVM
                              {
                                  Name = b.Name,
                                  Pages = b.Pages,
                                  Rating = b.Rating,
                                  Price = b.Price,
                                  UrlFolder = b.UrlImage,
                                  CategoryName = c.Name,
                                  PublicationDate = b.PublicationDate,
                                  PublisherName = p.Name,
                                  Authors = (from ba in _context.BookAuthors
                                             join a in _context.Authors
                                             on ba.IdAuthor equals a.Id
                                             where ba.IdBook == id
                                             select a.Name).ToList()
                              }
                              ).SingleOrDefaultAsync();
            if (book != null)
            {
                book.Urls = GetUrls(book.UrlFolder);
                return book;
            }
            else return default;
             
        }
    }
}
