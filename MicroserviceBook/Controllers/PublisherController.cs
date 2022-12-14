using MicroserviceBook.DTOs.Publisher;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.PublisherVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {

        public readonly IPublisherRepository _repo;

        public PublisherController(IPublisherRepository repo)
        {
            _repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            return Ok(await _repo.GetAllPublisherAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            return Ok(await _repo.GetPublisherAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(CreatePublisherDTO model)
        {
            return Ok(await _repo.CreatePublisher(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher([FromBody] UpdatePublisherDTO model)
        {
            return Ok(await _repo.UpdatePublisher(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            return Ok(await _repo.DeletePublisher(id));
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetPublisherByName(string? name)
        {
            var list = await _repo.getPublisherByName(name);
            var res = new List<GetPublisherVM>();
            if (list.Count == 0)
            {
                return Ok(res);
            }
            foreach (var i in list)
            {
                var temp = await _repo.GetPublisherAsync(i);
                res.Add(temp);
            }
            return Ok(res);
        }
    }
}
