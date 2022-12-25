using AutoMapper;
using MicroserviceOrder.Data;
using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.Services;
using MicroserviceOrder.ViewModels.OrderDetailVM;
using MicroserviceOrder.ViewModels.OrderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;
using System;
using System.Globalization;

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
            var order = await _context.Orders.Where(b => b.IsDeleted == false).Include(b => b.Payment).FirstOrDefaultAsync(c => c.Id == id);
            var result = order == null ? null : _mapper.Map<GetOrderVM>(order);
            result.OrderDetails = await _orderDetailRepository.GetOrderDetailByOrderIdAsync(id);
            return result;
        }

        public async Task<int> UpdateOrder(UpdateOrderDTO model)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (order == null)
                return -1;
            order.Status = model.Status;
            order.IdUser = model.IdUser;
            order.IdPayment = model.IdPayment;
            order.OrderAddress = model.OrderAddress;
            order.ReceiverName = model.ReceiverName;
            order.Number = model.Number;
            order.Total = model.Total;

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
        public async Task<bool> ChangeStatus(int id, bool status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == id);
            if (order == null)
                return false;
            order.Status = status;
            await _context.SaveChangesAsync();
            return order.Status;
        }

        public async Task<IEnumerable<GetOrderVM>> GetOrdersByUser(string? id)
        {
            var orders = await _context.Orders.Where(b => b.IsDeleted == false).Where(b=>b.IdUser==id).Include(b => b.Payment).ToListAsync();
            var results = orders.Select(i => _mapper.Map<GetOrderVM>(i));
            List<GetOrderVM> res = new List<GetOrderVM>();
            foreach (var order in orders)
            {
                var temp = order == null ? null : _mapper.Map<GetOrderVM>(order);
                temp.OrderDetails = await _orderDetailRepository.GetOrderDetailByOrderIdAsync(order.Id);
                res.Add(temp);
            }
            return res;
        }

        public async Task<IEnumerable<TotalSalesVM>> GetMonthlySales(DateTime startDate, DateTime endDate)
        {
            List<TotalSalesVM> monthlySalesList = new List<TotalSalesVM>();
            int count = 0;
            DateTime temp = startDate;
            while (temp <= endDate)
            {
                var orders = await _context.Orders.Where(
                            c => c.CreatedDate.Year == temp.Year
                            && c.CreatedDate.Month == temp.Month).Where(b => b.IsDeleted == false).ToListAsync();
                double monthlyTotal = 0;
                foreach (var order in orders)
                {
                    monthlyTotal += float.Parse(order.Total, CultureInfo.InvariantCulture.NumberFormat);
                }
                TotalSalesVM model = new TotalSalesVM();
                model.Time = temp.Month + "/" + temp.Year;
                model.Sales = monthlyTotal;
                monthlySalesList.Add(model);
                count++;
                temp = temp.AddMonths(1);

            }
            return monthlySalesList;
        }
        public async Task<IEnumerable<TotalSalesVM>> GetDailySales(DateTime startDate, DateTime endDate)
        {
            List<TotalSalesVM> dailySalesList = new List<TotalSalesVM>();
            int count = 0;
            DateTime temp = startDate;
            while (temp <= endDate)
            {
                var orders = await _context.Orders.Where(
                            c => c.CreatedDate.Date == temp.Date).Where(b => b.IsDeleted == false).ToListAsync();
                double dailyTotal = 0;
                foreach (var order in orders)
                {
                    dailyTotal += float.Parse(order.Total, CultureInfo.InvariantCulture.NumberFormat);
                }
                TotalSalesVM model = new TotalSalesVM();
                model.Time = temp.Date.ToString();
                model.Sales = dailyTotal;
                dailySalesList.Add(model);
                count++;
                temp = temp.AddDays(1);

            }
            return dailySalesList;
        }
        public async Task<IEnumerable<TotalSalesVM>> GetYearlySales(DateTime startDate, DateTime endDate)
        {
            List<TotalSalesVM> yearlySalesList = new List<TotalSalesVM>();
            int count = 0;
            DateTime temp = startDate;
            while (temp <= endDate)
            {
                var orders = await _context.Orders.Where(
                            c => c.CreatedDate.Year== temp.Year).Where(b => b.IsDeleted == false).ToListAsync();
                double yearlyTotal = 0;
                foreach (var order in orders)
                {
                    yearlyTotal += float.Parse(order.Total, CultureInfo.InvariantCulture.NumberFormat);
                }
                TotalSalesVM model = new TotalSalesVM();
                model.Time = temp.Year.ToString();
                model.Sales = yearlyTotal;
                yearlySalesList.Add(model);
                count++;
                temp = temp.AddYears(1);

            }
            return yearlySalesList;
        }
        public async Task<IEnumerable<TotalSalesVM>> GetWeeklySales(DateTime startDate, DateTime endDate)
        {
            List<TotalSalesVM> weeklySalesList = new List<TotalSalesVM>();
            int count = 0;
            DateTime temp = startDate;
            while (temp <= endDate)
            {
                count++;
                var orders = await _context.Orders.Where(
                            c => c.CreatedDate.Date >= temp.Date && c.CreatedDate.Date < temp.AddDays(7).Date)
                            .Where(b => b.IsDeleted == false).ToListAsync();
                double weeklyTotal = 0;
                foreach (var order in orders)
                {
                    weeklyTotal += float.Parse(order.Total, CultureInfo.InvariantCulture.NumberFormat);
                }
                TotalSalesVM model = new TotalSalesVM();
                model.Time = "Week "+ count;
                model.Sales = weeklyTotal;
                weeklySalesList.Add(model);
                
                temp = temp.AddDays(7);

            }
            return weeklySalesList;
        }
        public async Task<int> GetTotalOrdersDaily(DateTime date)
        {
            int total = await _context.Orders.Where(c => c.CreatedDate.Date == date.Date).Where(b => b.IsDeleted == false).CountAsync();
            return total;
        }
        public async Task<int> GetTotalPendingOrders()
        {
            int total = await _context.Orders.Where(c => c.Status == false).Where(b => b.IsDeleted == false).CountAsync();
            return total;
        }

        public async Task<double> DailyPaypalIncome(DateTime date)
        {
            var income = await _context.Orders.Where(c => c.CreatedDate == date.Date)
                .Where(c => c.IdPayment == 2).Where(b => b.IsDeleted == false).Select(c => c.Total).ToListAsync();
            double sum = 0;
            foreach(var i in income)
            {
                double temp = Convert.ToDouble(i);
                sum += temp;
            }
            return sum;
        }
    }
}
