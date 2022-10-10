using MicroserviceAccount.DTOs;
using MicroserviceAccount.ViewModels;

namespace MicroserviceAccount.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> RegisterAsync(RegisterDTO model);

        Task<AuthenticationVM> LoginAsync(LoginDTO model);

        Task<string> AddRoleAsync(AddRoleDTO model);

        Task<AuthenticationVM> ForgetPassword(ForgetPasswordDTO model);

        Task<AuthenticationVM> ResetPassword(ResetPasswordDTO model);

        Task<AuthenticationVM> ChangePassword(string email, ChangePasswordDTO model);
    }
}