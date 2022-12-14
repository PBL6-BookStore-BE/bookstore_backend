using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly ICartDetailRepository _repo;
        public CartDetailController(ICartDetailRepository repository)
        {
            _repo = repository;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]

        public async Task<IActionResult> CreateCart(CartDetailDTO model)
        {
            return Ok(await _repo.CreateCartDetail(model));
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]

        public async Task<IActionResult> GetCartByUser()
        {
            return Ok(await _repo.GetCartByUser());
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem (DeleteCartDTO model)
        {
            return Ok(await _repo.DeleteCartDetail(model));
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public async Task<IActionResult> ChangeQuantity (CartDetailDTO model)
        {
            return Ok(await _repo.ChangeQuantity(model));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteCart()
        {
            return Ok(await _repo.DeleteAllAsync());
        }

    }
}
