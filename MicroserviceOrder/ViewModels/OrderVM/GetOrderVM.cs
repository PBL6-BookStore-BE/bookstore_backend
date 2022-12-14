using MicroserviceOrder.ViewModels.OrderDetailVM;

namespace MicroserviceOrder.ViewModels.OrderVM
{
    public class GetOrderVM
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string IdUser { get; set; }
        public string Payment { get; set; }
        public string? OrderAddress { get; set; }
        public IEnumerable<GetOrderDetailVM>? OrderDetails { get; set; }
    }
}
