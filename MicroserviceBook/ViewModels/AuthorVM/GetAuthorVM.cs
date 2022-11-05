using MicroserviceBook.ViewModels.BookVM;

namespace MicroserviceBook.ViewModels.AuthorVM
{
    public class GetAuthorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<GetBookVM> Books { get; set; }
        public IEnumerable<int> BookIds { get; set; }
    }
}
