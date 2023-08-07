using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService: IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; set; }
        public EmailService(EmailSettings emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings ?? throw new ArgumentNullException(nameof(emailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
   
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGrid.SendGridClient(_emailSettings.ApiKey); 
            var subject = email.Subject;
            var to = new SendGrid.Helpers.Mail.EmailAddress(email.To);
            var body = email.Body;
            var from = new SendGrid.Helpers.Mail.EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };
            var sendGridMessage = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation($"Email sent.");

            if (response.Result.StatusCode == System.Net.HttpStatusCode.Accepted || response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Email sending failed.");
                return false;
            }
        }
    }
}
