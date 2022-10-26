namespace MicroserviceAccount.DTOs.Review
{
    public class AddReviewDTO
    {
        public int IdBook { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
