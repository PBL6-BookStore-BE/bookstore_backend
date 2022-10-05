namespace MicroserviceBook.Entities
{
    public class Book: BaseEntity
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public int Rating { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
    }
}
