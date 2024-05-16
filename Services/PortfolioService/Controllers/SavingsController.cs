using Common.Enums;
using Common.Exceptions;
using Common.Models.ProductModels.Properties;
using Common.Models.Savings;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Db;

namespace PortfolioService.Controllers
{
	/// <summary>
	/// Controller for managing savings.
	/// </summary>
	[Route("api/savings")]
	[ApiController]
	public class SavingsController
	{
		private readonly IDbService<Savings> _dbService;

		/// <summary>
		/// Initializes a new instance of the <see cref="SavingsController"/> class.
		/// </summary>
		/// <param name="dbService">The database service.</param>
		public SavingsController(IDbService<Savings> dbService)
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
				Savings savings = await _dbService.GetAsync(userId);
				res.Data = savings.Amount;
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
		/// Creates a new savings.
		/// </summary>
		/// <param name="createSavings">The savings creation request.</param>
		/// <returns>A boolean indicating if the creation was successful.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> AddSavings([FromBody] CreateSavings createSavings)
		{
			ArgumentNullException.ThrowIfNull(createSavings);

			Savings savings =
				new()
				{
					OwnerId = createSavings.OwnerId,
					Amount = createSavings.Amount,
					CreatedAt = DateTime.Now,
				};

			BaseResponse<bool> res = new();
			try
			{
				bool success = await _dbService.CreateAsync(savings);
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
		/// <param name="updateSavings">The update request.</param>
		/// <returns>A response indicating the success of the operation.</returns>
		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdateSavings([FromBody] UpdateSavings updateSavings)
		{
			BaseResponse<bool> res = new();
			try
			{
				Savings savings = await _dbService.GetAsync(updateSavings.Id);
				savings.Amount = updateSavings.Amount;

				bool success = await _dbService.UpdateAsync(savings);
				res.Data = success;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToUpdateException<Savings> ex)
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
		/// <param name="id">The savings Id.</param>
		/// <returns>A response indicating the success of the operation.</returns>
		[HttpDelete]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> DeleteSavings(int id)
		{
			BaseResponse<bool> res = new();
			try
			{
				bool success = await _dbService.DeleteAsync(id);
				res.Data = success;
				res.Status = EHttpStatus.OK;
				res.ResponseMessage = string.Empty;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToDeleteException<Savings> ex)
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
