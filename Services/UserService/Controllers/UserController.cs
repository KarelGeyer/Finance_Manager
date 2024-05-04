using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Service;

namespace UserService.Controllers
{
    /// <summary>
    /// Controller for managing user related operations.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDbService _dbService;

        public UserController(IDbService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns> A list of users.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<User>>> GetAll()
        {
            BaseResponse<List<User>> res = new();

            try
            {
                List<User> users = await _dbService.GetAll();
                res.Data = users;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Get a specific user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<User>> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            BaseResponse<User> res = new();

            try
            {
                User user = await _dbService.GetById(id);
                res.Data = user;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="req">The user creation request.</param>
        /// <returns>True if the user is created successfully, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"></exception>"
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> Create([FromBody] CreateUser req)
        {
            if (
                req == null
                || string.IsNullOrWhiteSpace(req.Name)
                || string.IsNullOrWhiteSpace(req.Surname)
                || string.IsNullOrWhiteSpace(req.Username)
                || string.IsNullOrWhiteSpace(req.Email)
                || string.IsNullOrWhiteSpace(req.Password)
            )
            {
                throw new ArgumentNullException(nameof(req));
            }

            var passwordHasher = new PasswordHasher<CreateUser>();
            req.Password = passwordHasher.HashPassword(req, req.Password);

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.Create(req);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToCreateException<User> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="req">The user update request.</param>
        /// <returns> True if the user is updated successfully, otherwise false. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> Update([FromBody] UpdateUser req)
        {
            if (
                req == null
                || string.IsNullOrWhiteSpace(req.Username)
                || string.IsNullOrWhiteSpace(req.Email)
            )
            {
                throw new ArgumentNullException(nameof(req));
            }

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.Update(req);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToUpdateException<User> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Update a user's password.
        /// </summary>
        /// <param name="req"> The user update password request.</param>
        /// <returns> True if the user's password is updated successfully, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdatePassword([FromBody] UpdateUserPassword req)
        {
            if (
                req == null
                || string.IsNullOrWhiteSpace(req.Password)
                || string.IsNullOrWhiteSpace(req.NewPassword)
            )
            {
                throw new ArgumentNullException(nameof(req));
            }

            User existingUser = await this._dbService.GetById(req.Id);

            if (existingUser == null)
            {
                throw new NotFoundException();
            }

            var passwordHasher = new PasswordHasher<User>();

            if (
                passwordHasher.VerifyHashedPassword(
                    existingUser,
                    existingUser.Password,
                    req.Password
                ) != PasswordVerificationResult.Success
            )
            {
                throw new ArgumentException("Invalid current password.");
            }

            req.Password = passwordHasher.HashPassword(existingUser, req.NewPassword);

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.UpdatePassword(req);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToUpdateException<User> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id"> The user ID.</param>
        /// <returns> True if the user is deleted successfully, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.Delete(id);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToDeleteException<User> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }
    }
}
