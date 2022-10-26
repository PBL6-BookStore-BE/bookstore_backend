using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
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
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _repository.GetAllOrdersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO model)
        {
            return Ok(await _repository.CreateOrder(model));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await _repository.DeleteOrder(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            return Ok(await _repository.GetOrder(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO model)
        {
            return Ok(await _repository.UpdateOrder(model));
        }
    }
}
