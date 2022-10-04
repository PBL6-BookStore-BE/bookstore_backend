using DAL.Interfaces.IBookRepositories;
using Microsoft.AspNetCore.Mvc;
using PBL6.BookStore.Models.DTOs.Book.BookDTO;

namespace Service.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _repo.GetAllBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _repo.GetBook(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDTO model)
        {
            return Ok(await _repo.CreateBook(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO model)
        {
            return Ok(await _repo.UpdateBook(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _repo.DeleteBook(id));
        }
    }
}