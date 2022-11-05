namespace MicroserviceBook.DTOs.Review
{
    public class CreateReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int IdBook { get; set; }

    }
}
