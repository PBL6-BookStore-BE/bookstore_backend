namespace MicroserviceBook.Service
{
    public interface IPictureService
    {
        public string UploadFile(List<IFormFile> list_img);
        public IEnumerable<string> GetUrls(string url_folder);
    }
}
