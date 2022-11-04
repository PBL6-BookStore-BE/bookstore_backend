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
            //var IdUser = _currentUserService.Id;
            //var url = "https://localhost:7075/gateway/carts";
            //var client = new RestClient(url);
            //var request = new RestRequest(url, Method.Post);
            //request.RequestFormat = DataFormat.Json;
            ////request.AddBody(IdUser);
            //RestResponse response = await client.ExecuteAsync(request);
            //var Output = response.Content;
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
        [Authorize(Roles = "Administrator,Customer", AuthenticationSchemes = "Bearer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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


        [HttpPost("administrator/user")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateUser(CreateUserDTO model)
        {
            var result = await _repo.CreateUser(model);
            return Ok(result);
        }

        [HttpGet("administrator/user")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _repo.GetAllUsers();
            return Ok(result);
        }
    }
}