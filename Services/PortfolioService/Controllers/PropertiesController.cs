using Common.Enums;
using Common.Exceptions;
using Common.Models.ProductModels.Properties;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPortfolioCommonService<Property> _portfolioCommonService;
        private readonly ICommonService<Property> _commonService;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(
            IPortfolioCommonService<Property> portfolioCommonService,
            ICommonService<Property> commonService,
            ILogger<PropertiesController> logger
        )
        {
            _portfolioCommonService = portfolioCommonService;
            _commonService = commonService;
            _logger = logger;
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
        public async Task<BaseResponse<List<Property>>> GetAllProperties(int ownerId)
        {
            _logger.LogInformation($"Getting all properties for ownerId: {ownerId}");
            BaseResponse<List<Property>> res = new();

            if (ownerId == 0)
                throw new ArgumentNullException(nameof(ownerId));

            try
            {
                List<Property> properties = await _commonService.GetEntities(ownerId);
                res.Data = properties;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetAllProperties)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetAllProperties)} - {res.Status} - {ex.Message}");
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
            _logger.LogInformation($"Getting property with id: {id}");
            BaseResponse<Property> res = new();
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            try
            {
                Property property = await _commonService.GetEntity(id);
                res.Data = property;
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
                _logger.LogError($"{nameof(GetProperty)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetProperty)} - {res.Status} - {ex.Message}");
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
            _logger.LogInformation($"Getting properties for ownerId: {ownerId} by categoryId: {categoryId}");
            BaseResponse<List<Property>> res = new();

            try
            {
                List<Property> properties = await _portfolioCommonService.GetCommonPortfolioEntitiesByCategory(ownerId, categoryId);
                res.Data = properties;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetPropertiesByCategory)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetPropertiesByCategory)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="propertyToBeCreated">The property creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateProperty([FromBody] Property propertyToBeCreated)
        {
            _logger.LogInformation($"Creating a new property");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.CreateEntity(propertyToBeCreated);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToCreateException<Property> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(CreateProperty)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(CreateProperty)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Update the name of an property.
        /// </summary>
        /// <param name="updateProperty">The property update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateProperty([FromBody] Property updateProperty)
        {
            _logger.LogInformation($"Updating property with id: {updateProperty.Id}");
            ArgumentNullException.ThrowIfNull(updateProperty);
            ArgumentNullException.ThrowIfNull(updateProperty.Name);

            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.UpdateEntity(updateProperty);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToUpdateException<Property> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(UpdateProperty)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(UpdateProperty)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Delete an property.
        /// </summary>
        /// <param name="id">The property ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteProperty(int id)
        {
            _logger.LogInformation($"Deleting property with id: {id}");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.DeleteEntity(id);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToDeleteException<Property> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(DeleteProperty)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(DeleteProperty)} - {res.Status} - {ex.Message}");
            }

            return res;
        }
    }
}
