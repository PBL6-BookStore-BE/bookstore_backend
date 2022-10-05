namespace MicroserviceBook.Entities
{
    public class BookAuthor : BaseEntity
    {
        public int IdBook { get; set; }
        public int IdAuthor { get; set; }
    }
}