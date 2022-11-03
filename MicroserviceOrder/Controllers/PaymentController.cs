using MicroserviceOrder.DTOs.Payment;
using MicroserviceOrder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        public readonly IPaymentRepository _repository;

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await _repository.GetAllPaymentsAsync());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDTO model)
        {
            return Ok(await _repository.CreatePayment(model));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            return Ok(await _repository.DeletePayment(id));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            return Ok(await _repository.GetPayment(id));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentDTO model)
        {
            return Ok(await _repository.UpdatePayment(model));
        }
    }
}
