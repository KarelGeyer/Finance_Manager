using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Services;

namespace UserService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthorizationService _authService;

		public AuthController(AuthorizationService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<string>> Login([FromBody] Login login)
		{
			BaseResponse<string> response = new();

			try
			{
				string token = await _authService.Login(login.Username, login.Password);
				response.Data = token;
				response.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				response.Status = EHttpStatus.NOT_FOUND;
				response.ResponseMessage = ex.Message;
			}
			catch (Exception ex)
			{
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
				response.ResponseMessage = ex.Message;
			}

			return response;
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> ValidateUserLogin()
		{
			BaseResponse<bool> response = new();

			try
			{

			}
		}
	}
}
