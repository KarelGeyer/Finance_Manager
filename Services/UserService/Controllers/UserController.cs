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

		/// <summary>
		/// Gets user by ID
		/// </summary>
		/// <param name="id">user's id</param>
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

		/// <summary>
		/// Attempts to retrieve a user from database by id
		/// </summary>
		/// <param name="username">username</param>
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

		/// <summary>
		/// Attempts to update User's data
		/// </summary>
		/// <param name="updateUser">user to update</param>
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

		/// <summary>
		/// Attempts to change user's password
		/// </summary>
		/// <param name="updatePassword">update password</param>
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

		/// <summary>
		/// Verify whetever user account has already been verified
		/// </summary>
		/// <param name="id">User's id</param>
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

		/// <summary>
		/// Attempts to delete user's account
		/// </summary>
		/// <param name="id">User's ID</param>
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

		/// <summary>
		/// Attempts to create a new user account
		/// </summary>
		/// <param name="newUser">new user</param>
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
