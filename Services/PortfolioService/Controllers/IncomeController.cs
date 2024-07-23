using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Models.ProductModels.Income;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Controllers
{
    /// <summary>
    /// Controller for managing income related operations.
    /// </summary>
    [Route("api/income")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IPortfolioCommonService<Income> _portfolioCommonService;
        private readonly ICommonService<Income> _commonService;
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(
            IPortfolioCommonService<Income> portfolioCommonService,
            ICommonService<Income> commonService,
            ILogger<IncomeController> logger
        )
        {
            _commonService = commonService;
            _portfolioCommonService = portfolioCommonService;
            _logger = logger;
        }

        /// <summary>
        /// Get all incomes for a specific user.
        /// </summary>
        /// <param name="ownerId">The user ID.</param>
        /// <returns>A list of incomes.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Income>>> GetAllIncomes(int ownerId)
        {
            _logger.LogInformation($"Getting all incomes for user {ownerId}");
            BaseResponse<List<Income>> res = new();

            try
            {
                List<Income> incomes = await _commonService.GetEntities(ownerId);
                res.Data = incomes;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error getting all incomes for user {ownerId}: {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Get a specific income for a user.
        /// </summary>
        /// <param name="id">The income ID.</param>
        /// <returns>The income.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Income>> GetIncome(int id)
        {
            _logger.LogInformation($"Getting income with ID {id}");
            BaseResponse<Income> res = new();

            try
            {
                Income income = await _commonService.GetEntity(id);
                res.Data = income;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Income with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error getting income with ID {id}: {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Get all incomes for a specific user by a given category.
        /// </summary>
        /// <param name="ownerId">The owner ID</param>
        /// <param name="categoryId">The category ID</param>
        /// <returns>A list of incomes.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Income>>> GetIncomesByCategory(int ownerId, int categoryId)
        {
            _logger.LogInformation($"Getting all incomes for user {ownerId} by category {categoryId}");
            BaseResponse<List<Income>> res = new();

            try
            {
                List<Income> properties = await _portfolioCommonService.GetCommonPortfolioEntitiesByCategory(ownerId, categoryId);
                res.Data = properties;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Invalid arguments for getting incomes by category for user {ownerId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error getting incomes by category for user {ownerId}: {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Create a new income.
        /// </summary>
        /// <param name="incomeToBeCreated">The income creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateIncome([FromBody] Income incomeToBeCreated)
        {
            _logger.LogInformation($"Creating a new income");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.CreateEntity(incomeToBeCreated);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToCreateException<Income> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error creating income: {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error creating income: {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Update the name of an income.
        /// </summary>
        /// <param name="updateIncome">The income update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateIncome([FromBody] Income updateIncome)
        {
            _logger.LogInformation($"Updating income with ID {updateIncome.Id}");
            ArgumentNullException.ThrowIfNull(updateIncome);
            ArgumentNullException.ThrowIfNull(updateIncome.Name);

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.UpdateEntity(updateIncome);
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
                _logger.LogError($"Error updating income with ID {updateIncome.Id}: {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error updating income with ID {updateIncome.Id}: {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Delete an income.
        /// </summary>
        /// <param name="id">The income ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteIncome(int id)
        {
            _logger.LogInformation($"Deleting income with ID {id}");
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
                _logger.LogError($"Error deleting income with ID {id}: {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"Error deleting income with ID {id}: {ex.Message}");
            }

            return res;
        }
    }
}
