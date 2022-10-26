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
        public async Task<IActionResult> CreateBook([FromForm] BookWithAuthorsDTO model)
        {
            return Ok(await _repo.CreateBook(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _repo.GetBook(id));
        }


        [HttpGet("Top10Rating")]
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

        //[HttpPost]
        //[Route("upload")]
        //public async Task<IActionResult> UploadImage(List<IFormFile> files)
        //{
        //    return Ok(await _repo.UploadFile(files));
        //    //long size = files.Sum(f => f.Length);

        //    //// full path to file in temp location
        //    //var filePath = Path.GetTempFileName();

        //    //foreach (var formFile in files)
        //    //{
        //    //    if (formFile.Length > 0)
        //    //    {
        //    //        using (var stream = new FileStream(filePath, FileMode.Create))
        //    //        {
        //    //            await formFile.CopyToAsync(stream);
        //    //        }
        //    //    }
        //    //}

        //    //// process uploaded files
        //    //// Don't rely on or trust the FileName property without validation.

        //    //return Ok(new { count = files.Count, size, filePath });
        //}
    }
}