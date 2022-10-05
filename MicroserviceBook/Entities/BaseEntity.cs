namespace MicroserviceBook.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public DateTime DeletedDate { get; set; }
    }
}
