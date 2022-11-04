using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MicroserviceBook.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string _email;
        private string _id;
        private string _username;

        public string Email
        {
            get
            {
                _email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? "";
                return _email;
            }
        }

        public string Id
        {
            get
            {
                _id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid) ?? "";
                return _id;

            }
        }

        public string Username
        {
            get
            {
                _username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                return _username;

            }
        }



    }
}
