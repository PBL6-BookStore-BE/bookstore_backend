namespace MicroserviceBook.Service
{
    public interface IPictureService
    {
        public Task<string> UploadFile(List<IFormFile> list_img);
        public Task<IEnumerable<string>> GetUrls(string url_folder);
    }
}
