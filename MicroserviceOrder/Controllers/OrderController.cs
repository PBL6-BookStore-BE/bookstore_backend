using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.ViewModels.OrderVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderRepository _repository;
        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _repository.GetAllOrdersAsync());
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO model)
        {
            return Ok(await _repository.CreateOrder(model));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await _repository.DeleteOrder(id));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            return Ok(await _repository.GetOrder(id));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO model)
        {
            return Ok(await _repository.UpdateOrder(model));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("changeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeOrderStatusDTO model)
        {
            return Ok(await _repository.ChangeStatus(model.Id, model.Status));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("getOrdersByUser")]
        public async Task<IActionResult> GetOrdersByUser(int id)
        {
            return Ok(await _repository.GetOrdersByUser(id));
        }
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Administrator")]
        [HttpPost("getMonthlySales")]
        public async Task<IActionResult> GetMonthlySales(SalesSearch model)
        {
            return Ok(await _repository.GetMonthlySales(model.StartTime,model.EndTime));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost("getDailySales")]
        public async Task<IActionResult> GetDailySales(SalesSearch model)
        {
            return Ok(await _repository.GetDailySales(model.StartTime, model.EndTime));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost("getYearlySales")]
        public async Task<IActionResult> GetYearlySales(SalesSearch model)
        {
            return Ok(await _repository.GetYearlySales(model.StartTime, model.EndTime));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost("getWeeklySales")]
        public async Task<IActionResult> GetWeeklySales(SalesSearch model)
        {
            return Ok(await _repository.GetWeeklySales(model.StartTime, model.EndTime));
        }

    }
}
