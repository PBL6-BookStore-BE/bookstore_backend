using MicroserviceAccount.DTOs;
using MicroserviceAccount.ViewModels;

namespace MicroserviceAccount.Interfaces
{
    public interface IAccountRepository
    {
        Task<AuthenticationVM> RegisterAsync(RegisterDTO model);

        Task<AuthenticationVM> LoginAsync(LoginDTO model);

        Task<AuthenticationVM> ForgetPassword(ForgetPasswordDTO model);

        Task<AuthenticationVM> ResetPassword(ResetPasswordDTO model);

        Task<AuthenticationVM> ChangePassword(string email, ChangePasswordDTO model);

        Task<AuthenticationVM> CreateUser(CreateUserDTO model);

        Task<IEnumerable<GetAllUsersVM>> GetAllUsers();

        Task<GetUserVM> GetUserByEmail(String email);
    }
}