using MicroserviceOrder.DTOs.OrderDetail;

namespace MicroserviceOrder.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string IdUser { get; set; }
        public int IdPayment { get; set; }
        public string? OrderAddress { get; set; }
        public string? ReceiverName { get; set; }
        public string? Number { get; set; }
        public IEnumerable<UpdateOrderDetailDTO>? OrderDetails { get; set; }
    }
}
