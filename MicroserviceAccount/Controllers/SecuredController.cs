using MicroserviceAccount.DTOs;
using MicroserviceAccount.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        private readonly IAccountRepository _repo;

        public SecuredController(IAccountRepository repo)
        {
            _repo = repo;
        }

        //[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        //[HttpPost]
        //public async Task<IActionResult> PostSecuredData()
        //{
        //    return Ok("This Secured Data is available only for Administrator");
        //}

        [HttpPost("AddRole")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddRoleAsync(AddRoleDTO model)
        {
            var result = await _repo.AddRoleAsync(model);
            return Ok(result);
        }
    }
}