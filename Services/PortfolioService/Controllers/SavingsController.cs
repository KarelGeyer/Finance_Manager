using Common.Enums;
using Common.Exceptions;
using Common.Models.PortfolioModels.Budget;
using Common.Models.Savings;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Controllers
{
	/// <summary>
	/// Controller for managing savings.
	/// </summary>
	[Route("api/savings")]
	[ApiController]
	public class SavingsController
	{
		private readonly ICommonService<Savings> _commonService;

		/// <summary>
		/// Initializes a new instance of the <see cref="SavingsController"/> class.
		/// </summary>
		/// <param name="dbService">The database service.</param>
		public SavingsController(ICommonService<Savings> commonService)
		{
			_commonService = commonService;
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
				Savings savings = await _commonService.GetEntity(userId);
				res.Data = savings.Amount;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
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
		/// Creates a new savings.
		/// </summary>
		/// <param name="savingsToBeCreated">The savings creation request.</param>
		/// <returns>A boolean indicating if the creation was successful.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> AddSavings([FromBody] Savings savingsToBeCreated)
		{
			BaseResponse<bool> res = new();
			try
			{
				bool success = await _commonService.CreateEntity(savingsToBeCreated);
				res.Data = success;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
			}
			catch (Exception ex) when (ex is FailedToCreateException<Savings> || ex is ArgumentException || ex is ArgumentNullException)
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
		/// <param name="updateSavings">The update request.</param>
		/// <returns>A response indicating the success of the operation.</returns>
		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdateSavings([FromBody] Savings updateSavings)
		{
			BaseResponse<bool> res = new();
			try
			{
				bool success = await _commonService.UpdateEntity(updateSavings);
				res.Data = success;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
			}
			catch (Exception ex)
				when (ex is NotFoundException || ex is FailedToUpdateException<Budget> || ex is ArgumentException || ex is ArgumentNullException)
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
		/// Deletes the savings for a user.
		/// </summary>
		/// <param name="id">The savings Id.</param>
		/// <returns>A response indicating the success of the operation.</returns>
		[HttpDelete]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> DeleteSavings(int id)
		{
			BaseResponse<bool> res = new();
			try
			{
				bool success = await _commonService.DeleteEntity(id);
				res.Data = success;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
			}
			catch (Exception ex)
				when (ex is NotFoundException || ex is FailedToDeleteException<Budget> || ex is ArgumentException || ex is ArgumentNullException)
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
