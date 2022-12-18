using MicroserviceAccount.DTOs;
using MicroserviceAccount.DTOs.Review;
using MicroserviceAccount.Interfaces;
using MicroserviceAccount.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MicroserviceAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly ICurrentUserService _currentUserService;

        public AccountController(IAccountRepository repo, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _currentUserService = currentUserService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDTO model)
        {
            var result = await _repo.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(LoginDTO model)
        {
            var result = await _repo.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            var result = await _repo.ForgetPassword(model);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var result = await _repo.ResetPassword(model);
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var email = _currentUserService.Email;
            var result = await _repo.ChangePassword(email, model);
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(String email)
        {
            var result = await _repo.GetUserByEmail(email);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("review")]
        public async Task<IActionResult> CreateReview([FromForm] AddReviewDTO model)
        {
            var url = "https://localhost:7075/gateway/review";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.RequestFormat = DataFormat.Json;
            var review = new ReviewDTO
            {
                IdBook = model.IdBook,
                Rating = model.Rating,
                Comment = model.Comment,
                IdUser = _currentUserService.Id
            };
            request.AddBody(
                review
                );
            RestResponse response = await client.ExecuteAsync(request);
            var Output = response.Content;
            return Ok(Output);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateUser(CreateUserDTO model)
        {
            var result = await _repo.CreateUser(model);
            return Ok(result);
        }

        [HttpGet("customers")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetListCustomers(string? email, string? phone)
        {
            var result = await _repo.GetListCustomers(email, phone);
            return Ok(result);
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetListAdmins(string? email, string? phone)
        {
            var result = await _repo.GetListAdmins(email, phone);
            return Ok(result);
        }

        [HttpPut("user")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateStateUser(string id, bool state)
        {
            var result = await _repo.UpdateStateUser(id, state);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO model)
        {
            var id = _currentUserService.Id;
            var result = await _repo.UpdateUser(id, model);
            return Ok(result);
        }

        [HttpGet("total-customer-account")]
        public async Task<IActionResult> GetTotalAccount()
        {
            var result = await _repo.GetTotalAccount();
            return Ok(result);
        }
    }
}