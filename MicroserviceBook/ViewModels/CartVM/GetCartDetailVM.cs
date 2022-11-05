using MicroserviceBook.ViewModels.BookVM;

namespace MicroserviceBook.ViewModels.CartVM
{
    public class GetCartDetailVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public  GetBookVM bookVM { get; set; }
    }
}
