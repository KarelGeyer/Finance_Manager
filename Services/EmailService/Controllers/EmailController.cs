using Common.Enums;
using Common.Models.Email;
using Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmailController : ControllerBase
	{
		private readonly ICommunicationService _emailService;

		public EmailController(ICommunicationService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost]
		[Route("[action]")]
		public BaseResponse<EmailResponse> SendEmail([FromBody] Email email)
		{
			ArgumentNullException.ThrowIfNull(email);

			BaseResponse<EmailResponse> res = new();

			try
			{
				/// Not gonna work, needs to configure the email service first
				res.Data = _emailService.SendEmail(email);
				res.Status = EHttpStatus.OK;
			}
			catch (Exception ex)
			{
				res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
				res.ResponseMessage = ex.Message;
			}

			return res;
		}
	}
}
