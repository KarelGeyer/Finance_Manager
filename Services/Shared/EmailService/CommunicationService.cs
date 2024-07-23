using Common.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace EmailService
{
    public class CommunicationService : ICommunicationService
    {
        private EmailConfigration _emailConfiguration;
        private MailboxAddress _emailAddress;
        private readonly ILogger<CommunicationService> _logger;

        public CommunicationService(EmailConfigration emailConfiguration, ILogger<CommunicationService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _emailAddress = MailboxAddress.Parse(_emailConfiguration.Address);
            _logger = logger;
        }

        public EmailResponse SendEmail(Email email)
        {
            _logger.LogInformation($"{nameof(SendEmail)} - method start");
            MimeMessage newEmail = new();
            EmailResponse response = new();

            newEmail.From.Add(_emailAddress);
            newEmail.To.Add(MailboxAddress.Parse(email.To));
            newEmail.Subject = email.Subject;
            newEmail.Body = new TextPart(TextFormat.Html) { Text = email.Body };
            SmtpClient client = new();

            try
            {
                _logger.LogInformation($"{nameof(SendEmail)} - attempting to connect to the mail server and send the email");
                client.Connect(_emailConfiguration.Smtp, _emailConfiguration.Port, _emailConfiguration.ShouldUseSSL);
                client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                client.Send(newEmail);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                _logger.LogError($"{nameof(SendEmail)} - ${ex.Message}");
            }

            client.Disconnect(true);
            client.Dispose();
            response.IsSuccess = true;
            response.Message = "Ok";

            return response;
        }
    }
}
