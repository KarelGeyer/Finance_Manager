using Common.Models.Email;

namespace EmailService
{
	public interface ICommunicationService
	{
		public EmailResponse SendEmail(Email email);
	}
}
