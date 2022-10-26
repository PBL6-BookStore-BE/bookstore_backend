using AutoMapper;
using MicroserviceBook.Entities;
using MicroserviceOrder.Data;
using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.ViewModels.OrderDetailVM;
using MicroserviceOrder.ViewModels.OrderVM;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDataContext _context;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderRepository(OrderDataContext context, IMapper mapper, IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<int> CreateOrder(CreateOrderDTO model)
        {
            var OrderEntity = _mapper.Map<Order>(model);
            _context.Orders.Add(OrderEntity);
            
            if (model.OrderDetails!=null)
            {
                foreach (var i in model.OrderDetails)
                {
                    _context.OrderDetails.Add(_mapper.Map<OrderDetail>(i));
                }
            }
            await _context.SaveChangesAsync();
            return OrderEntity.Id;
        }

        public async Task<int> DeleteOrder(int id)
        {
            var OrderEntity = await _context.Orders.FirstOrDefaultAsync(c => c.Id == id);
            if (OrderEntity == null)
            {
                return -1;
            }
            else
            {
                OrderEntity.IsDeleted = true;
                OrderEntity.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return OrderEntity.Id;
            }
        }

        public async Task<IEnumerable<GetAllOrdersVM>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.Where(b => b.IsDeleted == false).ToListAsync();
            var results = orders.Select(i => _mapper.Map<GetAllOrdersVM>(i));
            return results;
        }

        public async Task<GetOrderVM> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == id);
            var result = order == null ? null : _mapper.Map<GetOrderVM>(order);
            result.OrderDetails = await _orderDetailRepository.GetOrderDetailByOrderIdAsync(id);
            return result;
        }

        public async Task<int> UpdateOrder(UpdateOrderDTO model)
        {
            throw new NotImplementedException();
            if (model == null)
                return -1;
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (order == null)
                return -1;
            order.Status = model.Status;
            order.IdUser = model.IdUser;
            order.IdPayment = model.IdPayment;

            foreach (var i in model.OrderDetails)
            {
                var orderdetail = _context.OrderDetails.FirstOrDefaultAsync(c=>c.Id == i.Id);
                if (orderdetail==null)
                {
                    await _orderDetailRepository.CreateOrderDetail(_mapper.Map<CreateOrderDetailDTO>(i));
                }
                else
                {
                    await _orderDetailRepository.UpdateOrderDetail(i);
                }
            }
            await _context.SaveChangesAsync();
            return order.Id;
            
        }
    }
}
