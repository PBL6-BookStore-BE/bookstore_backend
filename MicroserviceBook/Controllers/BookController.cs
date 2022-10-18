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

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _repo.GetAllBooks());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookWithAuthorsDTO model)
        {
            return Ok(await _repo.CreateBook(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _repo.GetBook(id));
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

    }
}