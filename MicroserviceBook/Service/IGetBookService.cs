using MicroserviceBook.ViewModels.BookVM;

namespace MicroserviceBook.Service
{
    public interface IGetBookService
    {
        public Task<GetBookVM> GetBookById(int id);
        //public string UploadFile(List<IFormFile> list_img);
        //public IEnumerable<string> GetUrls(string url_folder);
    }
}
