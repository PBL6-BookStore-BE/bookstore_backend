namespace MicroserviceBook.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int IdBook { get; set; }
        public int IdUser { get; set; }
    }
}