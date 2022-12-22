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
        [HttpPut("byid")]
        public async Task<IActionResult> ChangeQuantityById(CartDetailDTO model)
        {
            var res = (await _repo.ChangeQuantity(model));
            if (res == 0) return BadRequest();
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteCart()
        {
            return Ok(await _repo.DeleteAllAsync());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("byid")]
        public async Task<IActionResult> DeleteCartItemById(DeleteCartDTO model)
        {
            var res = await _repo.DeleteCartDetailsById(model.Id);
            if (res == 0) return BadRequest();
            return Ok(res);
        }

    }
}
