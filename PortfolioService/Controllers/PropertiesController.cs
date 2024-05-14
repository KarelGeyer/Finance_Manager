using Common.Enums;
using Common.Exceptions;
using Common.Models.Income;
using Common.Models.PortfolioModels.Properties;
using Common.Models.ProductModels.Properties;
using Common.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Db;

namespace PortfolioService.Controllers
{
    [Route("api/properties")]
    [ApiController]
	public class PropertiesConrtoller : ControllerBase
	{
		private readonly IDbService<Property> _dbService;

		public PropertiesConrtoller(IDbService<Property> dbService)
		{
			_dbService = dbService;
		}

        /// <summary>
        /// Get all properties for a specific user.
        /// </summary>
        /// <param name="ownerId">The owner ID</param>
        /// <param name="month">The month</param>
        /// <param name="year">The year</param>
        /// <returns>A list of properties.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Property>>> GetAllProperties(int ownerId, int month, int year)
		{
			BaseResponse<List<Property>> res = new();

			if (month == 0 || month > 12)
				throw new ArgumentNullException(nameof(month));
			if (year < 1900 || year > DateTime.Now.Year)
				throw new ArgumentNullException(nameof(year));
			if (ownerId == 0)
				throw new ArgumentNullException(nameof(ownerId));

			try
			{
				List<Property> properties = await _dbService.GetAllAsync(ownerId, month, year);
				res.Data = properties;
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
        /// Get a specific properties for a user.
        /// </summary>
        /// <param name="id">The loan ID.</param>
        /// <returns>The property.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Property>> GetProperty(int id)
		{
			BaseResponse<Property> res = new();
			if (id == 0)
				throw new ArgumentNullException(nameof(id));

			try
			{
				Property property = await _dbService.GetAsync(id);
				res.Data = property;
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
        /// Get all properties for a specific user by a given category.
        /// </summary>
        /// <param name="ownerId">The owner ID</param>
        /// <param name="categoryId">The category ID</param>
        /// <returns>A list of properties.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Property>>> GetPropertiesByCategory(int ownerId, int categoryId)
		{
			BaseResponse<List<Property>> res = new();

			DateTime date = DateTime.Now;
			int month = date.Month;
			int year = date.Year;

			try
			{
				List<Property> properties = await _dbService.GetAllAsync(ownerId, month, year);
				res.Data = properties.Where(p => p.CategoryId == categoryId).ToList();
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
        /// Creates a new property.
        /// </summary>
        /// <param name="createProperty">The property creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateProperty([FromBody] CreateProperty createProperty)
		{
			ArgumentNullException.ThrowIfNull(createProperty);
			ArgumentNullException.ThrowIfNull(createProperty.Name);

			BaseResponse<bool> res = new();

			Property newProperty =
				new()
				{
					Name = createProperty.Name,
					Value = createProperty.Value,
					CategoryId = createProperty.CategoryId,
					OwnerId = createProperty.OwnerId,
					CreatedAt = DateTime.Now,
				};

			try
			{
				bool result = await _dbService.CreateAsync(newProperty);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (FailedToCreateException<Property> ex)
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
        /// Update the name of an property.
        /// </summary>
        /// <param name="updateLoan">The property update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateProperty([FromBody] UpdateProperty updateProperty)
		{
			ArgumentNullException.ThrowIfNull(updateProperty);
			ArgumentNullException.ThrowIfNull(updateProperty.Name);

			BaseResponse<bool> res = new();

			try
			{
				Property foundProperty = await _dbService.GetAsync(updateProperty.Id);
				foundProperty.Name = updateProperty.Name;
				foundProperty.Value = updateProperty.Value;

				bool result = await _dbService.UpdateAsync(foundProperty);
				res.Data = result;
				res.Status = EHttpStatus.OK;
			}
			catch (NotFoundException ex)
			{
				res.Data = false;
				res.Status = EHttpStatus.NOT_FOUND;
				res.ResponseMessage = ex.Message;
			}
			catch (FailedToUpdateException<Property> ex)
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
        /// Delete an property.
        /// </summary>
        /// <param name="loanId">The property ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteProperty(int id)
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
			catch (FailedToDeleteException<Property> ex)
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
