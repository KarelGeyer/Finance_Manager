using Common.Enums;
using Common.Exceptions;
using Common.Models.Expenses;
using Common.Models.ProductModels.Loans;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loanService;
        private readonly ICommonService<Loan> _commonService;
        private readonly IPortfolioCommonService<Loan> _portfolioCommonService;

        public LoansController(ILoansService loanService, ICommonService<Loan> commonService, IPortfolioCommonService<Loan> portfolioCommonService)
        {
            _loanService = loanService;
            _commonService = commonService;
            _portfolioCommonService = portfolioCommonService;
        }

        /// <summary>
        /// Get all loans for a specific user.
        /// </summary>
        /// <param name="ownerId">The owner ID</param>
        /// <param name="month">The month</param>
        /// <param name="year">The year</param>
        /// <returns>A list of loans.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetAllLoans(int ownerId, int month, int year)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                List<Loan> loans = await _commonService.GetEntities(ownerId, month, year);
                res.Data = loans;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
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
        /// Get all loans for a range of users represented by <paramref name="ids"/>.
        /// </summary>
        /// <param name="ids">array of user id's</param>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetAllLoansByGroup(int[] ids)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                List<Loan> loans = await _portfolioCommonService.GetAllPortfolioEntitiesByGroup(ids);
                res.Data = loans;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
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
        /// Get a specific loan for a user.
        /// </summary>
        /// <param name="loanId">The loan ID.</param>
        /// <returns>The loan.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Loan>> GetLoan(int loanId)
        {
            BaseResponse<Loan> res = new();

            try
            {
                Loan loan = await _commonService.GetEntity(loanId);
                res.Data = loan;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is NotFoundException || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
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
        /// Get all loans for a specific user.
        /// </summary>
        /// <param name="ownerId">The owner ID</param>
        /// <param name="ownToId">User id who a loan is owned to</param>
        /// <returns>A list of loans.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Loan>>> GetAllLoansByCreditor(int ownerId, int ownToId)
        {
            BaseResponse<List<Loan>> res = new();

            try
            {
                List<Loan> loans = await _loanService.GetLoansByOwnTo(ownerId, ownToId);
                res.Data = loans;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
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
        /// Get the total debt of a user.
        /// </summary>
        /// <param name="ownerId">User id</param>
        /// <returns>A sum user owns in total</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<double>> GetTotalDebt(int ownerId)
        {
            BaseResponse<double> res = new();

            try
            {
                double debt = await _loanService.GetTotalDebt(ownerId);
                res.Data = debt;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = 0;
                res.Status = EHttpStatus.BAD_REQUEST;
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
        /// Get the total debt user owns to another user or a an entity.
        /// </summary>
        /// <param name="ownerId">User Id</param>
        /// <param name="ownToId">Debtor Id</param>
        /// <returns>A sum user owns to a creditor</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<double>> GetTotalDebtByCreditor(int ownerId, int ownToId)
        {
            BaseResponse<double> res = new();

            try
            {
                double debt = await _loanService.GetTotalDebt(ownerId, ownToId);
                res.Data = debt;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = 0;
                res.Status = EHttpStatus.BAD_REQUEST;
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
        /// Creates a new loan.
        /// </summary>
        /// <param name="loanToBeCreated">The loan creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateLoan([FromBody] Loan loanToBeCreated)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.CreateEntity(loanToBeCreated);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToDeleteException<Expense> || ex is ArgumentException || ex is ArgumentNullException)
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
        /// Update the name of an loan.
        /// </summary>
        /// <param name="updateLoan">The loan update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateLoan(Loan updateLoan)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.UpdateEntity(updateLoan);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToUpdateException<Expense> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
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
        /// Delete an loan.
        /// </summary>
        /// <param name="loanId">The loan ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteLoan(int loanId)
        {
            BaseResponse<bool> res = new();

            try
            {
                var result = await _commonService.DeleteEntity(loanId);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToDeleteException<Expense> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
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
