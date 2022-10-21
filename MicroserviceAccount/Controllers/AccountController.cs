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

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterDTO model)
        {
            var result = await _repo.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync(LoginDTO model)
        {
            var result = await _repo.LoginAsync(model);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            var result = await _repo.ForgetPassword(model);
            return Ok(result);
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword( ResetPasswordDTO model)
        {
            var email = "dsd";
            var result = await _repo.ResetPassword(model);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var email = _currentUserService.Email;
            var result = await _repo.ChangePassword(email, model);
            return Ok(result);
        }

        [HttpGet("bookt")]
        public async Task<IActionResult> BookAcc()
        {
            var url = "https://localhost:7075/gateway/book";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            var Output = response.Content;
            return Ok(Output);
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

    }
}