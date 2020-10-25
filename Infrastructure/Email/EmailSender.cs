using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SendInBlueSettings> _settings;
        public EmailSender(IOptions<SendInBlueSettings> settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string userEmail, string emailSubject, string message)
        {
            Configuration.Default.AddApiKey("api-key", _settings.Value.Key);
            var client = new SMTPApi();
            var msg = new SendSmtpEmail(
                new SendSmtpEmailSender(_settings.Value.User, "angel_andoney@live.com.mx"),
                new List<SendSmtpEmailTo> { new SendSmtpEmailTo(userEmail) },
                htmlContent: message,
                textContent: message,
                subject: emailSubject
            );

            await client.SendTransacEmailAsync(msg);
        }
    }
}