using Common.Enums;
using Common.Exceptions;
using Common.Models.ProductModels.Income;
using Common.Response;
using IncomeService.Db;
using Microsoft.AspNetCore.Mvc;

namespace IncomeService.Controllers
{
	/// <summary>
	/// Controller for managing income related operations.
	/// </summary>
	[Route("api/income")]
	[ApiController]
	public class IncomeController : ControllerBase
	{
		private readonly IDbService _dbService;

		public IncomeController(IDbService dbService)
		{
			_dbService = dbService;
		}

		/// <summary>
		/// Get all incomes for a specific user.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <returns>A list of incomes.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<Income>>> GetAll(int userId)
		{
			BaseResponse<List<Income>> res = new();

			try
			{
				List<Income> incomes = await _dbService.GetAll(userId);
				res.Data = incomes;
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
		/// Get a specific income for a user.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="id">The income ID.</param>
		/// <returns>The income.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<Income>> Get(int userId, int id)
		{
			BaseResponse<Income> res = new();

			try
			{
				Income income = await _dbService.Get(userId, id);
				res.Data = income;
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
		/// Create a new income.
		/// </summary>
		/// <param name="req">The income creation request.</param>
		/// <returns>A boolean indicating if the creation was successful.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> Create([FromBody] CreateIncome req)
		{
			if (req == null)
				throw new ArgumentNullException();
			if (req.Name == string.Empty)
				throw new ArgumentNullException(req.Name);

			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.Create(req);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (FailedToCreateException<Income> ex)
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
		/// Update the name of an income.
		/// </summary>
		/// <param name="req">The income update name request.</param>
		/// <returns>A boolean indicating if the update was successful.</returns>
		[HttpPut]
		[Route("[action]/Name")]
		public async Task<BaseResponse<bool>> Update([FromBody] IncomeUpdateNameRequest req)
		{
			if (req == null)
				throw new ArgumentNullException();
			if (req.Name == string.Empty)
				throw new ArgumentNullException(req.Name);

			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.Update(req);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToUpdateException<Income> ex)
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
		/// Update the value of an income.
		/// </summary>
		/// <param name="req">The income update value request.</param>
		/// <returns>A boolean indicating if the update was successful.</returns>
		[HttpPut]
		[Route("[action]/Value")]
		public async Task<BaseResponse<bool>> Update([FromBody] IncomeUpdateValueRequest req)
		{
			if (req == null)
				throw new ArgumentNullException();

			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.Update(req);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToUpdateException<Income> ex)
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
		/// Delete an income.
		/// </summary>
		/// <param name="id">The income ID.</param>
		/// <returns>A boolean indicating if the deletion was successful.</returns>
		[HttpDelete]
		[Route("[action]/Value")]
		public async Task<BaseResponse<bool>> Delete(int id)
		{
			BaseResponse<bool> res = new();

			try
			{
				bool result = await _dbService.Delete(id);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToDeleteException<Income> ex)
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
