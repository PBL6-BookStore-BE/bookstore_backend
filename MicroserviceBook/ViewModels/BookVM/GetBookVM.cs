using MicroserviceBook.ViewModels.AuthorVM;

namespace MicroserviceBook.ViewModels.BookVM
{
    public class GetBookVM
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}
