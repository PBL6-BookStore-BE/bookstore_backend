﻿using MicroserviceOrder.DTOs.Order;
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
        public Task<IEnumerable<GetOrderVM>> GetOrdersByUser(string? id);
        public Task<IEnumerable<TotalSalesVM>> GetYearlySales(DateTime startDate, DateTime endDate);
        public Task<IEnumerable<TotalSalesVM>> GetMonthlySales(DateTime startDate, DateTime endDate);
        public Task<IEnumerable<TotalSalesVM>> GetWeeklySales(DateTime startDate, DateTime endDate);
        public Task<IEnumerable<TotalSalesVM>> GetDailySales(DateTime startDate, DateTime endDate);
        public Task<int> GetTotalOrdersDaily(DateTime date);
        public Task<int> GetTotalPendingOrders();
        public Task<double> DailyPaypalIncome(DateTime date);

    }
}
