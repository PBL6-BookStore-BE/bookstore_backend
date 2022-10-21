namespace MicroserviceAccount.DTOs.Review
{
    public class ReviewDTO
    {
        public int IdBook { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string  IdUser { get; set; }
    }
}
