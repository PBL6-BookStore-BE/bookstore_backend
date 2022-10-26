namespace MicroserviceOrder.DTOs.OrderDetail
{
    public class UpdateOrderDetailDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public int IdBook { get; set; }
    }
}
