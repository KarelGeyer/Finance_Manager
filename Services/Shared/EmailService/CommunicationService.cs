using Common.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace EmailService
{
    public class CommunicationService : ICommunicationService
    {
        private EmailConfigration _emailConfiguration;
        private MailboxAddress _emailAddress;

        public CommunicationService(EmailConfigration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
            _emailAddress = MailboxAddress.Parse(_emailConfiguration.Address);
        }

        public EmailResponse SendEmail(Email email)
        {
            MimeMessage newEmail = new();
            EmailResponse response = new();

            newEmail.From.Add(_emailAddress);
            newEmail.To.Add(MailboxAddress.Parse(email.To));
            newEmail.Subject = email.Subject;
            newEmail.Body = new TextPart(TextFormat.Html) { Text = email.Body };
            SmtpClient client = new();

            try
            {
                client.Connect(_emailConfiguration.Smtp, _emailConfiguration.Port, _emailConfiguration.ShouldUseSSL);
                client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                client.Send(newEmail);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            client.Disconnect(true);
            client.Dispose();
            response.IsSuccess = true;
            response.Message = "Ok";

            return response;
        }
    }
}
