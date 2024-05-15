using Common.Enums;
using Common.Exceptions;
using Common.Models.Expenses;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Db;

namespace PortfolioService.Controllers
{
    [Route("api/expenses")]
    [Route("[controller]")]
	public class ExpensesController : ControllerBase
	{
		private readonly ILogger<ExpensesController> _logger;
		private readonly IDbService<Expense> _dbService;

		public ExpensesController(ILogger<ExpensesController> logger, IDbService<Expense> dbService)
		{
			_logger = logger;
			_dbService = dbService;
		}

		/// <summary>
		/// Get all expenses for a specific user.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <returns>A list of expenses.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<Expense>>> GetAllExpenses(int ownerId)
		{
			BaseResponse<List<Expense>> res = new();

			try
			{
				List<Expense> expenses = await _dbService.GetAllAsync(ownerId);
				res.Data = expenses;
				res.Status = EHttpStatus.OK;
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
		/// Get a specific expense for a user.
		/// </summary>
		/// <param name="id">The income ID.</param>
		/// <returns>An expense.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<Expense>> GetExpense(int id)
		{
			BaseResponse<Expense> res = new();

			try
			{
				Expense expense = await _dbService.GetAsync(id);
				res.Data = expense;
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
        /// Create a new expense.
        /// </summary>
        /// <param name="createExpense">The expense creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> CreateExpense([FromBody] CreateExpense createExpense)
		{
			ArgumentNullException.ThrowIfNull(createExpense);
			ArgumentNullException.ThrowIfNull(createExpense.Name);

			Expense newExpense =
				new()
				{
					Name = createExpense.Name,
					Value = createExpense.Value,
					CategoryId = createExpense.CategoryId,
					OwnerId = createExpense.OwnerId,
					CreatedAt = DateTime.Now,
				};

			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.CreateAsync(newExpense);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (FailedToCreateException<Expense> ex)
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
        /// Update the name of an expense.
        /// </summary>
        /// <param name="updateExpense">The expense update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdateExpense([FromBody] UpdateExpense updateExpense)
		{
			ArgumentNullException.ThrowIfNull(updateExpense);
			ArgumentNullException.ThrowIfNull(updateExpense.Name);

			BaseResponse<bool> res = new();

			try
			{
				Expense expense = await _dbService.GetAsync(updateExpense.Id);
				expense.Name = updateExpense.Name;
				expense.Value = updateExpense.Value;

				bool result = await _dbService.UpdateAsync(expense);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToUpdateException<Expense> ex)
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
		/// Delete an expense.
		/// </summary>
		/// <param name="id">The expense ID.</param>
		/// <returns>A boolean indicating if the deletion was successful.</returns>
		[HttpDelete]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> DeleteExpense(int id)
		{
			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.DeleteAsync(id);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToDeleteException<Expense> ex)
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
