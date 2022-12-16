using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace MicroserviceBook.Service
{
    public class PictureService : IPictureService
    {
        public async Task<IEnumerable<string>> GetUrls(string url_folder)
        {
            string exp = "folder=" + url_folder;
            var list_url = new List<string>();
            Account account = new Account(
                  "dgs9vyh4n",
                  "759658434427383",
                  "oobrP1pOzKOb9q7E9vB_jBQqQHY");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            //var result = cloudinary.ListResources();
            SearchResult result = await cloudinary.Search()
                .Expression(exp)
                //.WithField("context")
                //.WithField("tags")
                .MaxResults(4)
                .ExecuteAsync();
            //var r = cloudinary.ListResourcesByAssetFolder(url);
            string s = "b8b7e61e-d1c9-4d00-944d-b81ea97fabc9";

            //var result = await cloudinary.ListResourceByAssetFolderAsync(s, false,false,false);
            //var result = await cloudinary.ListResourcesByAssetFolder

           

            //var listResourcesByPrefixParams = new ListResourcesByPrefixParams()
            //{
            //    Type = "upload",
            //    Prefix = "7cadf3cd-580e-4dda-9ee2-14391eb62e94"
            //};
            //var listResourcesResult = cloudinary.ListResources(listResourcesByPrefixParams);

            var k = result.Resources;

            foreach (var i in k)
            {
                list_url.Add(i.Url.ToString());
            }
            return list_url;
        }

        public async Task<string> UploadFile(List<IFormFile> list_img)
        {
            Account account = new Account(
                     "dgs9vyh4n",
                     "759658434427383",
                     "oobrP1pOzKOb9q7E9vB_jBQqQHY");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            var guiID = Guid.NewGuid();
            string rootFolder = "book/" + guiID;
      
            foreach (var img in list_img)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(img.FileName, img.OpenReadStream()),
                    PublicId = Path.GetFileNameWithoutExtension(img.FileName),
                    Folder = rootFolder,
                    AssetFolder = guiID.ToString(),
              };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
            return rootFolder;
        }
    }
}
