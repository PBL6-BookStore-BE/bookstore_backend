using MicroserviceBook.ViewModels.BookVM;

namespace MicroserviceBook.ViewModels.AuthorVM
{
    public class GetAuthorVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<GetAllBooksVM> Books { get; set; }
    }
}
