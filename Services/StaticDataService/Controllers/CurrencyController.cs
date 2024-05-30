using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Models.Currency;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using StaticDataService.Interfaces;

namespace StaticDataService.Controllers
{
	/// <summary>
	/// Represents a controller for managing currency-related operations.
	/// </summary>
	[Route("api/currency")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		private readonly ICommonService<Currency> _commonService;

		/// <summary>
		/// Initializes a new instance of the <see cref="CurrencyController"/> class.
		/// </summary>
		/// <param name="dbService">The database service.</param>
		public CurrencyController(ICommonService<Currency> staticDataCommonService)
		{
			_commonService = staticDataCommonService;
		}

		/// <summary>
		/// Retrieves all currencies.
		/// </summary>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<Currency>>> GetAllCurrencies()
		{
			BaseResponse<List<Currency>> res = new();

			try
			{
				List<Currency> currencies = await _commonService.GetEntities();
				res.Data = currencies;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = null;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (ArgumentNullException ex)
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
		/// Retrieves a currency by its ID.
		/// </summary>
		/// <param name="id">The ID of the currency.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<Currency>> GetCurrency(int id)
		{
			BaseResponse<Currency> res = new();

			try
			{
				Currency currency = await _commonService.GetEntity(id);
				res.Data = currency;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = null;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (ArgumentNullException ex)
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
	}
}
