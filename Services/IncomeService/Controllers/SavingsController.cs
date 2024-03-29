using Common.Enums;
using Common.Exceptions;
using Common.Models.Savings;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using SavingsService.Service;

namespace SavingsService.Controllers
{
    /// <summary>
    /// Controller for managing savings.
    /// </summary>
    [Route("api/income")]
    [ApiController]
    public class SavingsController
    {
        private readonly IDbService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsController"/> class.
        /// </summary>
        /// <param name="dbService">The database service.</param>
        public SavingsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Gets the savings for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The savings amount.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<double>> GetSavings(int userId)
        {
            BaseResponse<double> res = new();
            try
            {
                double amount = await _dbService.Get(userId);
                res.Data = amount;
                res.Status = EHttpStatus.OK;
                res.ResponseMessage = string.Empty;
            }
            catch (NotFoundException ex)
            {
                res.Data = 0;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = 0;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Adds savings for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> AddSavings(int userId)
        {
            BaseResponse<bool> res = new();
            try
            {
                bool success = await _dbService.Create(userId);
                res.Data = success;
                res.Status = EHttpStatus.OK;
                res.ResponseMessage = string.Empty;
            }
            catch (FailedToCreateException<Savings> ex)
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
        /// Updates the savings for a user.
        /// </summary>
        /// <param name="req">The update request.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateSavings([FromBody] UpdateSavings req)
        {
            BaseResponse<bool> res = new();
            try
            {
                bool success = await _dbService.Update(req);
                res.Data = success;
                res.Status = EHttpStatus.OK;
                res.ResponseMessage = string.Empty;
            }
            catch (FailedToUpdateException<Savings> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (UserDoesNotExistException ex)
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
        /// Deletes the savings for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteSavings(int userId)
        {
            BaseResponse<bool> res = new();
            try
            {
                bool success = await _dbService.Delete(userId);
                res.Data = success;
                res.Status = EHttpStatus.OK;
                res.ResponseMessage = string.Empty;
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
