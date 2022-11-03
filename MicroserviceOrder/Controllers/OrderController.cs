using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
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
    }
}
