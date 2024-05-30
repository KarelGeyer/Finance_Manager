using Common;
using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using StaticDataService.Interfaces;

namespace StaticDataService.Controllers
{
	[Route("api/categoryType")]
	[ApiController]
	public class CategoryTypeController : ControllerBase
	{
		private readonly ICommonService<CategoryType> _commonService;

		public CategoryTypeController(ICommonService<CategoryType> commonService)
		{
			_commonService = commonService;
		}

		/// <summary>
		/// List of <see cref="CategoryType"/> category types
		/// </summary>
		/// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="CategoryType"/> category type</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<List<CategoryType>>> GetAllCategoryTypes()
		{
			BaseResponse<List<CategoryType>> res = new();

			try
			{
				List<CategoryType> categoryTypes = await _commonService.GetEntities();
				res.Data = categoryTypes;
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
		/// <see cref="CategoryType"/> category type
		/// </summary>
		/// <param name="req">Id of a <see cref="CategoryType"/> category type</param>
		/// <returns><see cref="Task"/> with <see cref="CategoryType"/> category type</returns>
		[HttpGet]
		[Route("[action]")]
		public async Task<BaseResponse<CategoryType>> GetCategoryTypeById(int id)
		{
			BaseResponse<CategoryType> res = new();

			try
			{
				CategoryType categoryType = await _commonService.GetEntity(id);
				res.Data = categoryType;
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
