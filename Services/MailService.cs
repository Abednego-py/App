using App.Models;
using Microsoft.Extensions.Options;

namespace App.Services
{
    public class MailService : IMailService 
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public Task SendEmailAsync(MailRequest mailRequest)
        {
            throw new NotImplementedException();
        }
    }
}
