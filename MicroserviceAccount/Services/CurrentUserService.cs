using System.Security.Claims;

namespace MicroserviceAccount.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string _email;

        public string Email
        {
            get
            {
                _email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? "";
                return _email;
            }
        }
    }
}
