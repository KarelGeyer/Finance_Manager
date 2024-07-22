using Common.Models.Email;
using System.Net.Mail;

namespace EmailService
{
    public interface ICommunicationService
    {
        public EmailResponse SendEmail(Email email);
    }
}
