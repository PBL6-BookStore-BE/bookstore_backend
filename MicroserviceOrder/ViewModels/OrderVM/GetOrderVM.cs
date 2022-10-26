using MicroserviceOrder.ViewModels.OrderDetailVM;

namespace MicroserviceOrder.ViewModels.OrderVM
{
    public class GetOrderVM
    {
        public bool Id { get; set; }
        public bool Status { get; set; }
        public string User { get; set; }
        public string Payment { get; set; }
        public IEnumerable<GetOrderDetailVM>? OrderDetails { get; set; }
    }
}
