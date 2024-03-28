using Common.Enums;
using Common.Exceptions;
using Common.Models.Savings;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using SavingsService.Service;

namespace SavingsService.Controllers
{
    [Route("api/income")]
    [ApiController]
    public class SavingsController
    {
        private readonly IDbService _dbService;

        public SavingsController(IDbService dbService)
        {
            _dbService = dbService;
        }

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

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateSavings([FromBody] UpdateSavings req)
        {
            BaseResponse<bool> res = new();
            try
            {
                await _dbService.GetUserId(req.Id);
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
