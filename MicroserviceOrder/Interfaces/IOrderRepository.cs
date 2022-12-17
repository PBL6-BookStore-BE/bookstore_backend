using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.ViewModels.OrderVM;

namespace MicroserviceOrder.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<GetAllOrdersVM>> GetAllOrdersAsync();
        public Task<GetOrderVM> GetOrder(int id);

        public Task<int> CreateOrder(CreateOrderDTO model);

        public Task<int> UpdateOrder(UpdateOrderDTO model);
        public Task<int> DeleteOrder(int id);
        public Task<bool> ChangeStatus(int id, bool status);
        
    }
}
