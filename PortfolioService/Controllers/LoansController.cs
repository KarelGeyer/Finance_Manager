using Common.Enums;
using Common.Exceptions;
using Common.Models.ProductModels.Loans;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Db;

namespace PortfolioService.Controllers
{
	[Route("api/loans")]
	[ApiController]
	public class LoansController : ControllerBase
	{
		private readonly IDbService<Loan> _dbService;

		public LoansController(IDbService<Loan> dbService)
		{
			_dbService = dbService;
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

			if (month == 0 || month > 12)
				throw new ArgumentNullException(nameof(month));
			if (year < 1900 || year > DateTime.Now.Year)
				throw new ArgumentNullException(nameof(year));
			if (ownerId == 0)
				throw new ArgumentNullException(nameof(ownerId));

			try
			{
				List<Loan> loans = await _dbService.GetAllAsync(ownerId, month, year);
				res.Data = loans;
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
				Loan loan = await _dbService.GetAsync(loanId);
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

		/// <summary>
		/// Get all loans for a specific user.
		/// </summary>
		/// <param name="ownerId">The owner ID</param>
		/// <param name="month">The month</param>
		/// <param name="year">The year</param>
		/// <returns>A list of loans.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<Loan>>> GetAllLoansByCreditor(int ownerId, int ownToId)
		{
			BaseResponse<List<Loan>> res = new();

			try
			{
				List<Loan> loans = await _dbService.GetAllAsync(ownerId);
				res.Data = loans.Where(x => x.To == ownToId).ToList();
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
		/// Creates a new loan.
		/// </summary>
		/// <param name="createLoan">The loan creation request.</param>
		/// <returns>A boolean indicating if the creation was successful.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> CreateLoan([FromBody] CreateLoan createLoan)
		{
			BaseResponse<bool> res = new();

			Loan newLoan =
				new()
				{
					Name = createLoan.Name,
					To = createLoan.OwnToId,
					OwnerId = createLoan.OwnerId,
					Value = createLoan.Value,
					CreatedAt = DateTime.Now
				};

			try
			{
				bool result = await _dbService.CreateAsync(newLoan);
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

		/// <summary>
		/// Update the name of an loan.
		/// </summary>
		/// <param name="updateLoan">The loan update name request.</param>
		/// <returns>A boolean indicating if the update was successful.</returns>
		[HttpPut]
		[Route("[action]")]
		public async Task<BaseResponse<bool>> UpdateLoan(UpdateLoan updateLoan)
		{
			BaseResponse<bool> res = new();

			try
			{
				Loan loan = await _dbService.GetAsync(updateLoan.Id);
				loan.Name = updateLoan.Name;
				loan.Value = updateLoan.Value;

				bool result = await _dbService.UpdateAsync(loan);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
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
				var result = await _dbService.DeleteAsync(loanId);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
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
