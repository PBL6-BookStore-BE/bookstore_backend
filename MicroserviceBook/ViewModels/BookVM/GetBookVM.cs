using MicroserviceBook.ViewModels.AuthorVM;

namespace MicroserviceBook.ViewModels.BookVM
{
    public class GetBookVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Pulbisher { get; set; }
        public IEnumerable<GetAllAuthorsVM> Authors { get; set; }
    }
}
