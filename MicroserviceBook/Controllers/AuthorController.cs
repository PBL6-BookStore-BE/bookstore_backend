using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public readonly IAuthorRepository _repo;

        public AuthorController(IAuthorRepository repo)
        {
            _repo = repo;
        }
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _repo.GetAllAuthors());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            return Ok(await _repo.GetAuthor(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromForm] CreateAuthorDTO? model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));
            return Ok(await _repo.CreateAuthor(model));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDTO model)
        {
            return Ok(await _repo.UpdateAuthor(model));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _repo.DeleteAuthor(id));
        }
        [HttpGet("search")]
        public async Task<IActionResult> getAuthorByNameFilter(string? name)
        {
            return Ok(await _repo.GetAuthorByNameFilter(name));
        }


    }
}

