using MicroserviceBook.DTOs.Category;
using MicroserviceBook.Interfaces;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        public readonly IOrderDetailRepository _repository;
        public OrderDetailController(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            return Ok(await _repository.GetAllOrderDetailsAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailDTO model)
        {
            return Ok(await _repository.CreateOrderDetail(model));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            return Ok(await _repository.DeleteOrderDetail(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            return Ok(await _repository.GetOrderDetail(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailDTO model)
        {
            return Ok(await _repository.UpdateOrderDetail(model));
        }
    }
}
