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

    }
}