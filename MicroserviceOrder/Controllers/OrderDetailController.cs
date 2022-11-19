using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            return Ok(await _repository.GetAllOrderDetailsAsync());
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailDTO model)
        {
            return Ok(await _repository.CreateOrderDetail(model));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            return Ok(await _repository.DeleteOrderDetail(id));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            return Ok(await _repository.GetOrderDetail(id));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailDTO model)
        {
            return Ok(await _repository.UpdateOrderDetail(model));
        }
    }
}
