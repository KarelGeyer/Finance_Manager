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

		public ExpensesController(IPortfolioCommonService<Expense> portfolioCommonService, ICommonService<Expense> commonService)
		{
			_commonService = commonService;
			_portfolioCommonService = portfolioCommonService;
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
			BaseResponse<List<Expense>> res = new();

			try
			{
				List<Expense> expenses = await _commonService.GetEntities(ownerId, month, year);
				res.Data = expenses;
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
		/// Get all expenses for a specific user by category.
		/// </summary>
		/// <param name="ownerId">User ID</param>
		/// <param name="categoryId">Category Id</param>
		/// <returns>A list of expenses.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<Expense>>> GetAllExpensesByCategory(int ownerId, int categoryId)
		{
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
		/// Get all expenses for a specific user sorted by <paramref name="sortBy"/> and in order specified by <paramref name="shouldBeReversed"/>.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <param name="shouldBeReversed">wheter entities shoule be in reverse order or not</param>
		/// <param name="sortBy">specifiy a property to sort by, can be: name, value or date</param>
		/// <returns>A list of expenses.</returns>
		[HttpGet]
		[Route("[action]")]
		public Task<BaseResponse<List<Expense>>> GetAllExpensesSorted(int ownerId, bool shouldBeReversed, EPortfolioModelSortBy sortBy)
		{
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
		/// <param name="expenseToBeCreated">The expense creation request.</param>
		/// <returns>A boolean indicating if the creation was successful.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> CreateExpense([FromBody] Expense expenseToBeCreated)
		{
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
		public async Task<BaseResponse<bool>> UpdateExpense([FromBody] Expense updateExpense)
		{
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
