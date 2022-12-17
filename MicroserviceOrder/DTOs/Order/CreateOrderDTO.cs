
using MicroserviceOrder.DTOs.OrderDetail;

namespace MicroserviceOrder.DTOs.Order
{
    public class CreateOrderDTO
    {
        public bool Status { get; set; }
       // public string IdUser { get; set; }
        public int IdPayment { get; set; }
        public string? OrderAddress { get; set; }
        public string? ReceiverName { get; set; }
        public string? Number { get; set; }
        public string? Total { get; set; }
        public IEnumerable<CreateOrderDetailDTO>? OrderDetails { get; set; }
    }
}
