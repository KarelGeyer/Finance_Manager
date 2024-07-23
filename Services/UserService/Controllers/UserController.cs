using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using UserService.Controllers;
using UserService.Interfaces;

namespace UsersService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">user's id</param>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<UserResponse>> GetUserById(int id)
        {
            _logger.LogInformation($"{nameof(GetUserById)} - method start");
            BaseResponse<UserResponse> response = new BaseResponse<UserResponse>();
            try
            {
                UserResponse user = await _service.GetUser(id);
                response.Data = user;
                response.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.NOT_FOUND;
                _logger.LogError($"{nameof(GetUserById)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(GetUserById)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to retrieve a user from database by id
        /// </summary>
        /// <param name="username">username</param>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<UserResponse>> GetUserByUsername(string username)
        {
            _logger.LogInformation($"{nameof(GetUserByUsername)} - method start");
            BaseResponse<UserResponse> response = new BaseResponse<UserResponse>();
            try
            {
                UserResponse user = await _service.GetUser(username);
                response.Data = user;
                response.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.NOT_FOUND;
                _logger.LogError($"{nameof(GetUserByUsername)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(GetUserByUsername)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to retrieve a users from database by by provided userGroup
        /// </summary>
        /// <param name="username">username</param>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<UserGroup>> GetUsersByUserGroup(Guid userGroup)
        {
            _logger.LogInformation($"{nameof(GetUsersByUserGroup)} - method start");
            BaseResponse<UserGroup> response = new BaseResponse<UserGroup>();
            try
            {
                UserGroup group = await _service.GetUsersFromUserGroup(userGroup);
                response.Data = group;
                response.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.NOT_FOUND;
                _logger.LogError($"{nameof(GetUsersByUserGroup)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(GetUsersByUserGroup)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DoesUserGroupAlreadyExist(Guid userId)
        {
            _logger.LogInformation($"{nameof(DoesUserGroupAlreadyExist)} - method start");
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool result = await _service.DoesUserGroupAlreadyExist(userId);
                response.Data = result;
                response.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(DoesUserGroupAlreadyExist)} - {response.Status} - {ex.Message}");
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
            _logger.LogInformation($"{nameof(UpdateUser)} - method start");
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
                _logger.LogError($"{nameof(UpdateUser)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex) when (ex is Exception || ex is FailedToUpdateException<User>)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(UpdateUser)} - {response.Status} - {ex.Message}");
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
            _logger.LogInformation($"{nameof(UpdatePassword)} - method start");
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
                _logger.LogError($"{nameof(UpdatePassword)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(UpdatePassword)} - {response.Status} - {ex.Message}");
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
            _logger.LogInformation($"{nameof(VerifyUser)} - method start");
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
                _logger.LogError($"{nameof(VerifyUser)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(VerifyUser)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateUserGroup([FromBody] int[] userIds, Guid userGroup)
        {
            _logger.LogInformation($"{nameof(CreateUserGroup)} - method start");
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool result = await _service.CreateUserGroup(userIds, userGroup);
                response.Data = result;
                response.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToUpdateException<User> || ex is NotFoundException || ex is UserAlreadyExistsException)
            {
                response.Data = false;
                response.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(CreateUserGroup)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(CreateUserGroup)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> AddUserToGroup([FromBody] int userId, Guid userGroup)
        {
            _logger.LogInformation($"{nameof(AddUserToGroup)} - method start");
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool result = await _service.AddUserToUserGroup(userId, userGroup);
                response.Data = result;
                response.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToUpdateException<User> || ex is NotFoundException)
            {
                response.Data = false;
                response.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(AddUserToGroup)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(AddUserToGroup)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> RemoveUserFromGroup([FromBody] int userId)
        {
            _logger.LogInformation($"{nameof(RemoveUserFromGroup)} - method start");
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool result = await _service.RemoveUserFromUserGroup(userId);
                response.Data = result;
                response.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToUpdateException<User> || ex is NotFoundException)
            {
                response.Data = false;
                response.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(RemoveUserFromGroup)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(RemoveUserFromGroup)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteUserGroup(Guid userGroup)
        {
            _logger.LogInformation($"{nameof(DeleteUserGroup)} - method start");
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool result = await _service.DeleteUserGroup(userGroup);
                response.Data = result;
                response.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToUpdateException<User> || ex is NotFoundException)
            {
                response.Data = false;
                response.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(DeleteUserGroup)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(DeleteUserGroup)} - {response.Status} - {ex.Message}");
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
            _logger.LogInformation($"{nameof(DeleteUser)} - method start");
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
                _logger.LogError($"{nameof(DeleteUser)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(DeleteUser)} - {response.Status} - {ex.Message}");
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
            _logger.LogInformation($"{nameof(CreateUser)} - method start");
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
                _logger.LogError($"{nameof(CreateUser)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                _logger.LogError($"{nameof(CreateUser)} - {response.Status} - {ex.Message}");
            }

            return response;
        }
    }
}
