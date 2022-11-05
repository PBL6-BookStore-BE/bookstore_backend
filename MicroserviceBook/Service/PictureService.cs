using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace MicroserviceBook.Service
{
    public class PictureService : IPictureService
    {
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

        public string UploadFile(List<IFormFile> list_img)
        {
            Account account = new Account(
                     "dgs9vyh4n",
                     "759658434427383",
                     "oobrP1pOzKOb9q7E9vB_jBQqQHY");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            var guiID = Guid.NewGuid();
            string rootFolder = "book/" + guiID;
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
    }
}
