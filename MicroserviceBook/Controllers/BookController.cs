using MicroserviceBook.DTOs.Book;
using MicroserviceBook.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] BookWithAuthorsDTO model)
        {
            return Ok(await _repo.CreateBook(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _repo.GetBook(id));
        }


        [HttpGet("Top10")]
        public async Task<IActionResult> GetTop10ByRating()
        {
            return Ok(await _repo.Top10ByRating());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _repo.DeleteBook(id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO model)
        {
            return Ok(await _repo.UpdateBook(model));
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