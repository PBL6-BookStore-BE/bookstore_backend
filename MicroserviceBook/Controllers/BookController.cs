using MicroserviceBook.DTOs.Book;
using MicroserviceBook.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookRepository _repo;
        public readonly IPublisherRepository _pubRepo;
        public readonly ICategoryRepository _categoryRepo;
        public BookController(IBookRepository repo, IPublisherRepository pubRepo, ICategoryRepository categoryRepo)
        {
            _repo = repo;
            _pubRepo = pubRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _repo.GetAllBooks());
        }
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] BookWithAuthorsDTO model)
        {
            return Ok(await _repo.CreateBook(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var res = await _repo.GetBook(id);
            if (res == null) return NotFound();
            return Ok(await _repo.GetBook(id));
        }


        [HttpGet("Top10")]
        public async Task<IActionResult> GetTop10ByRating()
        {
            return Ok(await _repo.Top10ByRating());
        }

        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _repo.DeleteBook(id));
        }

        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookDTO? model)
        {
            if (model != null) {
                return Ok(await _repo.UpdateBook(model));
            }
            return BadRequest();
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBookByPriceFilter(decimal? lowest, decimal? highest, string? namePub, string? nameCate)
        {

            var IdPubs = await _pubRepo.getPublisherByName(namePub);
            var IdCates = await _categoryRepo.getCategoryByName(nameCate);
            
            return Ok(await _repo.Searchbook(lowest,highest,IdCates,IdPubs));
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetBookByName(string? nameBook)
        {
            return Ok(await _repo.GetBookByNameFilter(nameBook));
        }
    }
}