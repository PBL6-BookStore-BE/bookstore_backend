using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MicroserviceBook.Data;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Service
{
    public class GetBookService : IGetBookService
    {
        private readonly BookDataContext _context;
        private readonly IPictureService _service;
        public GetBookService(BookDataContext context, IPictureService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<GetBookVM> GetBookById(int id)
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
                                  Id = id,
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
                                             where (ba.IdBook == id && ba.IsDeleted == false)
                                             select a.Name).ToList()
                              }
                           ).SingleOrDefaultAsync();
            if (book != null)
            {
                book.Urls = await _service.GetUrls(book.UrlFolder);
                return book;
            }
            else return default;
        }

        //public string UploadFile(List<IFormFile> list_img)
        //{
        //    Account account = new Account(
        //            "dgs9vyh4n",
        //            "759658434427383",
        //            "oobrP1pOzKOb9q7E9vB_jBQqQHY");

        //    Cloudinary cloudinary = new Cloudinary(account);
        //    cloudinary.Api.Secure = true;

        //    var guiID = Guid.NewGuid();
        //    string rootFolder = "book/" + guiID + "/";
        //    string temp = "";
        //    foreach (var img in list_img)
        //    {
        //        temp = rootFolder + Path.GetFileNameWithoutExtension(img.FileName);
        //        var uploadParams = new ImageUploadParams()
        //        {
        //            File = new FileDescription(img.FileName, img.OpenReadStream()),
        //            PublicId = temp
        //        };
        //        var uploadResult = cloudinary.Upload(uploadParams);
        //    }
        //    return rootFolder;
        //}

        //public IEnumerable<string> GetUrls(string url_folder)
        //{
        //    var list_url = new List<string>();
        //    Account account = new Account(
        //          "dgs9vyh4n",
        //          "759658434427383",
        //          "oobrP1pOzKOb9q7E9vB_jBQqQHY");

        //    Cloudinary cloudinary = new Cloudinary(account);
        //    cloudinary.Api.Secure = true;

        //    //var result = cloudinary.ListResources();
        //    SearchResult result = cloudinary.Search()
        //        .Expression(url_folder)
        //        .WithField("context")
        //        .WithField("tags")
        //        .MaxResults(10)
        //        .Execute();

        //    var k = result.Resources;

        //    foreach (var i in k)
        //    {
        //        list_url.Add(i.Url.ToString());
        //    }
        //    return list_url;

        //}
    }
}
