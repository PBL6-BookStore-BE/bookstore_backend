namespace MicroserviceBook.ViewModels.ReviewVM
{
    public class GetReviewByBookId
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string IdUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
