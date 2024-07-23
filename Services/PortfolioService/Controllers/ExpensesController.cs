using Common.Enums;
using Common.Exceptions;
using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Interfaces.Services;
using PortfolioService.Services;

namespace PortfolioService.Controllers
{
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IPortfolioCommonService<Expense> _portfolioCommonService;
        private readonly ICommonService<Expense> _commonService;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(
            IPortfolioCommonService<Expense> portfolioCommonService,
            ICommonService<Expense> commonService,
            ILogger<ExpensesController> logger
        )
        {
            _commonService = commonService;
            _portfolioCommonService = portfolioCommonService;
            _logger = logger;
        }

        /// <summary>
        /// Get all expenses for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Expense>>> GetAllExpenses(int ownerId, int month, int year)
        {
            _logger.LogInformation($"{nameof(GetAllExpenses)} - method start");
            BaseResponse<List<Expense>> res = new();

            try
            {
                List<Expense> expenses = await _commonService.GetEntities(ownerId, month, year);
                res.Data = expenses;
                res.Status = EHttpStatus.OK;

                _logger.LogInformation($"{nameof(GetAllExpenses)} - OK");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpenses)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpenses)} - {res.Status} - {ex.Message}");
            }

            _logger.LogInformation($"{nameof(GetAllExpenses)} - method end");
            return res;
        }

        /// <summary>
        /// Get all expenses for a specific user by category.
        /// </summary>
        /// <param name="ownerId">User ID</param>
        /// <param name="categoryId">Category Id</param>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Expense>>> GetAllExpensesByCategory(int ownerId, int categoryId)
        {
            _logger.LogInformation($"{nameof(GetAllExpensesByCategory)} - method start");
            BaseResponse<List<Expense>> res = new();

            try
            {
                List<Expense> expenses = await _portfolioCommonService.GetCommonPortfolioEntitiesByCategory(ownerId, categoryId);
                res.Data = expenses;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesByCategory)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesByCategory)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Get all expenses for a specific user sorted by <paramref name="sortBy"/> and in order specified by <paramref name="shouldBeReversed"/>.
        /// </summary>
        /// <param name="ownerId">user Id</param>
        /// <param name="shouldBeReversed">wheter entities shoule be in reverse order or not</param>
        /// <param name="sortBy">specifiy a property to sort by, can be: name, value or date</param>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        [Route("[action]")]
        public BaseResponse<List<Expense>> GetAllExpensesSorted(int ownerId, bool shouldBeReversed, EPortfolioModelSortBy sortBy)
        {
            _logger.LogInformation($"{nameof(GetAllExpensesSorted)} - method start");
            BaseResponse<List<Expense>> res = new();

            try
            {
                List<Expense> expenses = _portfolioCommonService.GetCommonPortfolioEntitiesSortedByGivenParameter(ownerId, shouldBeReversed, sortBy);
                res.Data = expenses;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesSorted)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesSorted)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Get all expenses for a range of users represented by <paramref name="ids"/>.
        /// </summary>
        /// <param name="ids">array of user id's</param>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Expense>>> GetAllExpensesByGroup(int[] ids)
        {
            _logger.LogInformation($"{nameof(GetAllExpensesByGroup)} - method start");
            BaseResponse<List<Expense>> res = new();

            try
            {
                List<Expense> expenses = await _portfolioCommonService.GetAllPortfolioEntitiesByGroup(ids);
                res.Data = expenses;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesByGroup)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetAllExpensesByGroup)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Get a specific expense for a user.
        /// </summary>
        /// <param name="id">The income ID.</param>
        /// <returns>An expense.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Expense>> GetExpense(int id)
        {
            _logger.LogInformation($"{nameof(GetExpense)} - method start");
            BaseResponse<Expense> res = new();

            try
            {
                Expense expense = await _commonService.GetEntity(id);
                res.Data = expense;
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

                _logger.LogError($"{nameof(GetExpense)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetExpense)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Create a new expense.
        /// </summary>
        /// <param name="expenseToBeCreated">The expense creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateExpense([FromBody] Expense expenseToBeCreated)
        {
            _logger.LogInformation($"{nameof(CreateExpense)} - method start");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.CreateEntity(expenseToBeCreated);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToCreateException<Expense> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(CreateExpense)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(CreateExpense)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Update the name of an expense.
        /// </summary>
        /// <param name="updateExpense">The expense update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateExpense([FromBody] Expense updateExpense)
        {
            _logger.LogInformation($"{nameof(UpdateExpense)} - method start");
            ArgumentNullException.ThrowIfNull(updateExpense);
            ArgumentNullException.ThrowIfNull(updateExpense.Name);

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.UpdateEntity(updateExpense);
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

                _logger.LogError($"{nameof(UpdateExpense)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(UpdateExpense)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Delete an expense.
        /// </summary>
        /// <param name="id">The expense ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteExpense(int id)
        {
            _logger.LogInformation($"{nameof(DeleteExpense)} - method start");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.DeleteEntity(id);
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

                _logger.LogError($"{nameof(DeleteExpense)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(DeleteExpense)} - {res.Status} - {ex.Message}");
            }

            return res;
        }
    }
}
