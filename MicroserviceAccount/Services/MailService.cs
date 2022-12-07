using Microsoft.Extensions.Options;
using MyEmailSender;

namespace MicroserviceAccount.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSender _emailSender;

        public MailService(IOptions<MyMailSettings> myMailSettings)
        {
            if (myMailSettings != null)
            {
                _emailSender = new EmailSender(myMailSettings);
            }
        }

        public async Task<bool> ForgetPasswordSendMail(string toEmail, string username, string resetToken)
        {
            await _emailSender.ForgetPasswordSendMail(toEmail, username, resetToken);
            return true;
        }
    }
}