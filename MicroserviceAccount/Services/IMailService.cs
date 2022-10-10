namespace MicroserviceAccount.Services
{
    public interface IMailService
    {
        Task<bool> ForgetPasswordSendMail(string toEmail, string username, string resetToken);
    }
}