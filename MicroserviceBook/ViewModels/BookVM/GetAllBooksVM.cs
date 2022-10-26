namespace MicroserviceBook.ViewModels.BookVM
{
    public class GetAllBooksVM
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public decimal Rating { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
        public string UrlImage { get; set; }
    }

}
