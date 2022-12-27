using MicroserviceBook.DTOs.Book;
using MicroserviceBook.DTOs.Review;
using MicroserviceBook.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public readonly IReviewRepository _repo;

        public ReviewController(IReviewRepository repo)
        {
            _repo = repo;
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllReview()
        {
            return Ok(await _repo.GetAllReviewsAsync());
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDTO model)
        {
            return Ok(await _repo.CreateReview(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            return Ok(await _repo.GetReviewAsync(id));
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            return Ok(await _repo.DeleteReview(id));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO model)
        {
            return Ok(await _repo.UpdateReview(model));
        }

        [HttpGet("idBook/{idBook}")]
        public async Task<IActionResult> GetReviewByBookId(int idBook)
        {
            return Ok(await _repo.GetReviewsByIdBookAsync(idBook));
        }

    }
}
