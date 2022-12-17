using MicroserviceBook.DTOs.Author;
using MicroserviceBook.DTOs.Category;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _repository.GetAllCategoriesAsync());
        }
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO model)
        {
            return Ok(await _repository.CreateCategory(model));
        }
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            return Ok(await _repository.DeleteCategory(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var res = await _repository.GetCategory(id);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDTO model)
        {
            return Ok(await _repository.UpdateCategory(model));
        }


        [HttpGet("search")]
        public async Task<IActionResult> GetCategoryByName(string? name)
        {
            var list = await _repository.getCategoryByName(name);
            var res = new List<GetCategoryVM>();
            if (list.Count != 0)
            {
                foreach (var i in list)
                {
                    var temp = await _repository.GetCategory(i);
                    res.Add(temp);
                }
            }
            return Ok(res);
        }

    }
}
