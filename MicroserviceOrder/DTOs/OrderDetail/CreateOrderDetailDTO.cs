namespace MicroserviceOrder.DTOs.OrderDetail
{
    public class CreateOrderDetailDTO
    {
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public int IdBook { get; set; }
    }
}
