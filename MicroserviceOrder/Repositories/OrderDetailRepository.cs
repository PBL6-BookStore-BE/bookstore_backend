using AutoMapper;
using MicroserviceAccount.Services;
using MicroserviceBook.ViewModels.CategoryVM;
using MicroserviceOrder.Data;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.ViewModels.OrderDetailVM;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDataContext _context;
        private readonly IMapper _mapper;
        public OrderDetailRepository(OrderDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateOrderDetail(CreateOrderDetailDTO model)
        {
            var OrderDetailEntity = _mapper.Map<OrderDetail>(model);
            _context.OrderDetails.Add(OrderDetailEntity);
            await _context.SaveChangesAsync();
            return OrderDetailEntity.Id;
        }

        public async Task<int> DeleteOrderDetail(int id)
        {
            var OrderDetailEntity = await _context.OrderDetails.FirstOrDefaultAsync(c => c.Id == id);
            if (OrderDetailEntity == null)
            {
                return -1;
            }
            else
            {
                OrderDetailEntity.IsDeleted = true;
                OrderDetailEntity.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return OrderDetailEntity.Id;
            }
        }

        public async Task<IEnumerable<GetAllOrderDetailsVM>> GetAllOrderDetailsAsync()
        {
            var orderdetails = await _context.OrderDetails.Where(b => b.IsDeleted == false).ToListAsync();
            var results = orderdetails.Select(i => _mapper.Map<GetAllOrderDetailsVM>(i));
            return results;
        }

        public async Task<GetOrderDetailVM> GetOrderDetail(int id)
        {
            var orderdetail = await _context.OrderDetails.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (orderdetail == null)
                return null;
            var result = _mapper.Map<GetOrderDetailVM>(orderdetail);
            return result;
        }

        public async Task<IEnumerable<GetOrderDetailVM>> GetOrderDetailByOrderIdAsync(int id)
        {
            var orderdetails = await _context.OrderDetails.Where(i => i.IdOrder == id).ToListAsync();
            if (orderdetails == null)
            {
                return null;
            }
            
            var result = orderdetails.Select(i => _mapper.Map<GetOrderDetailVM>(i));
            return result;
        }

        public async Task<int> UpdateOrderDetail(UpdateOrderDetailDTO model)
        {
            if (model == null)
                return -1;

            var orderdetail = await _context.OrderDetails.Where(i => i.Id == model.Id).FirstOrDefaultAsync();
            if (orderdetail == null)
                return -1;
            orderdetail.IdBook = model.IdBook;
            orderdetail.IdOrder = model.IdOrder;
            orderdetail.Quantity = model.Quantity;
            await _context.SaveChangesAsync();
            return orderdetail.Id;
        }
    }
}
