using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;

namespace UsersService.Controllers
{
	[Route("api/v1")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _service;

		public UserController(IUserService service)
		{
			_service = service;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<User>> GetUserById(int id)
		{
			BaseResponse<User> response = new BaseResponse<User>();
			try
			{
				User user = await _service.GetUser(id);
				response.Data = user;
				response.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.NOT_FOUND;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<User>> GetUserByUsername(string username)
		{
			BaseResponse<User> response = new BaseResponse<User>();
			try
			{
				User user = await _service.GetUser(username);
				response.Data = user;
				response.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.NOT_FOUND;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdateUser([FromBody] UpdateUser updateUser)
		{
			BaseResponse<bool> response = new BaseResponse<bool>();
			try
			{
				bool result = await _service.UpdateUser(updateUser);
				response.Data = result;
				response.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.NOT_FOUND;
			}
			catch (Exception ex) when (ex is Exception || ex is FailedToUpdateException<User>)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdatePassword([FromBody] UpdatePassword updatePassword)
		{
			BaseResponse<bool> response = new BaseResponse<bool>();
			try
			{
				bool result = await _service.UpdatePassword(updatePassword);
				response.Data = result;
				response.Status = EHttpStatus.OK;
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FailedToUpdateException<User> || ex is NotFoundException)
			{
				response.Data = false;
				response.Status = ex switch
				{
					NotFoundException => EHttpStatus.NOT_FOUND,
					_ => EHttpStatus.BAD_REQUEST
				};
				response.ResponseMessage = ex.Message;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> VerifyUser(int id)
		{
			BaseResponse<bool> response = new BaseResponse<bool>();
			try
			{
				bool result = await _service.VerifyUser(id);
				response.Data = result;
				response.Status = EHttpStatus.OK;
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FailedToUpdateException<User> || ex is NotFoundException)
			{
				response.Data = false;
				response.Status = ex switch
				{
					NotFoundException => EHttpStatus.NOT_FOUND,
					_ => EHttpStatus.BAD_REQUEST
				};
				response.ResponseMessage = ex.Message;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpDelete]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> DeleteUser(int id)
		{
			BaseResponse<bool> response = new BaseResponse<bool>();
			try
			{
				bool result = await _service.DeleteUser(id);
				response.Data = result;
				response.Status = EHttpStatus.OK;
			}
			catch (FailedToDeleteException<User> ex)
			{
				response.Data = false;
				response.Status = EHttpStatus.BAD_REQUEST;
				response.ResponseMessage = ex.Message;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> CreateUser([FromBody] CreateUser createUser)
		{
			BaseResponse<bool> response = new BaseResponse<bool>();
			try
			{
				bool result = await _service.CreateUser(createUser);
				response.Data = result;
				response.Status = EHttpStatus.OK;
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FailedToCreateException<User> || ex is ArgumentException)
			{
				response.Data = false;
				response.Status = EHttpStatus.BAD_REQUEST;
				response.ResponseMessage = ex.Message;
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
			}

			return response;
		}
	}
}
