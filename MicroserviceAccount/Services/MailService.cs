using MailKit.Net.Smtp;
using MailKit.Security;
using MicroserviceAccount.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
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
            resetToken = "http://localhost:3000/reset-password/" + resetToken;
            await _emailSender.ForgetPasswordSendMail(toEmail, username, resetToken);
            return true;
        }
    }
}