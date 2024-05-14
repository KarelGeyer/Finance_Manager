using Common.Enums;
using Common.Exceptions;
using Common.Models.ProductModels.Loans;
using Common.Response;
using LoansService.Db;
using Microsoft.AspNetCore.Mvc;

namespace LoansService.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IDbService _dbService;

        public LoansController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetAll(int ownerId)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                var loans = await _dbService.GetAll(ownerId);
                res.Data = loans;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Loan>> GetById(int ownerId, int loanId)
        {
            BaseResponse<Loan> res = new();

            try
            {
                var loan = await _dbService.GetById(ownerId, loanId);
                res.Data = loan;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetByOwnToId(int ownerId, int ownToId)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                var loans = await _dbService.GetByOwnToId(ownerId, ownToId);
                res.Data = loans;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetByMonth(int ownerId, string month)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                var loans = await _dbService.GetByMonth(ownerId, month);
                res.Data = loans;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetByDate(int ownerId, DateTime date)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                var loans = await _dbService.GetByDate(ownerId, date);
                res.Data = loans;
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

        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> Create(CreateLoan createLoan)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.Create(createLoan);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToCreateException<Loan> ex)
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
        public async Task<BaseResponse<bool>> Update(UpdateLoan req)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.Update(req);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToUpdateException<Loan> ex)
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
        public async Task<BaseResponse<bool>> DeleteOne(int ownerId, int loanId)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.DeleteOne(ownerId, loanId);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToDeleteException<Loan> ex)
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
        public async Task<BaseResponse<bool>> DeleteAllToByMonth(
            int ownerId,
            int ownToId,
            string month
        )
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.DeleteAllToByMonth(ownerId, ownToId, month);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToDeleteException<Loan> ex)
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
        public async Task<BaseResponse<bool>> DeleteAllTo(int ownerId, int ownToId, string month)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.DeleteAllTo(ownerId, ownToId, month);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToDeleteException<Loan> ex)
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
        public async Task<BaseResponse<bool>> DeleteAllByMonth(int ownerId, string month)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _dbService.DeleteAllByMonth(ownerId, month);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (FailedToDeleteException<Loan> ex)
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
