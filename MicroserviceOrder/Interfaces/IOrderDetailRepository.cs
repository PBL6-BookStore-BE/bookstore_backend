using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.ViewModels.OrderDetailVM;
using MicroserviceOrder.ViewModels.OrderVM;

namespace MicroserviceOrder.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<IEnumerable<GetAllOrderDetailsVM>> GetAllOrderDetailsAsync();
        public Task<GetOrderDetailVM> GetOrderDetail(int id);

        public Task<int> CreateOrderDetail(CreateOrderDetailDTO model);

        public Task<int> UpdateOrderDetail(UpdateOrderDetailDTO model);
        public Task<int> DeleteOrderDetail(int id);
        public Task<IEnumerable<GetOrderDetailVM>> GetOrderDetailByOrderIdAsync(int id);
    }
}
