using AutoMapper;
using MicroserviceOrder.Data;
using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.Services;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrderRepository(OrderDataContext context, IMapper mapper, IOrderDetailRepository orderDetailRepository, ICurrentUserService currentUserService, IPaymentRepository paymentRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
            _currentUserService = currentUserService;
            _paymentRepository = paymentRepository;
        }

        public async Task<int> CreateOrder(CreateOrderDTO model)
        {
            var userId = _currentUserService.Id.ToString();
            var OrderEntity = _mapper.Map<Order>(model);
            OrderEntity.IdUser = userId;
            _context.Orders.Add(OrderEntity);
            await _context.SaveChangesAsync();
            if (model.OrderDetails!=null)
            {
                foreach (var i in model.OrderDetails)
                {
                    var orderDetail = _mapper.Map<OrderDetail>(i);
                    orderDetail.IdOrder = OrderEntity.Id;
                    _context.OrderDetails.Add(orderDetail);
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
                var orderdetails = await _orderDetailRepository.GetOrderDetailByOrderIdAsync(OrderEntity.Id);
                if (orderdetails != null)
                {
                    foreach (var c in orderdetails)
                    {
                        await _orderDetailRepository.DeleteOrderDetail(c.Id);
                    }
                }
                OrderEntity.IsDeleted = true;
                OrderEntity.DeletedDate = DateTime.Now;
                
                await _context.SaveChangesAsync();
                return OrderEntity.Id;
            }
        }

        public async Task<IEnumerable<GetAllOrdersVM>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.Where(b => b.IsDeleted == false).Include(b => b.Payment).ToListAsync();
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
            order.OrderAddress = model.OrderAddress;
            order.ReceiverName = model.ReceiverName;
            order.Number = model.Number;

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
