using MicroserviceBook.Entities;

namespace MicroserviceBook.DTOs.Cart
{
    public class CartDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public int IdCart { get; set; }
        public string IdBook { get; set; }
    }
}
