using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using StaticDataService.Interfaces;

namespace StaticDataService.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICommonService<Category> _commonService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICommonService<Category> staticDataCommonService,
            ICategoryService categoryService,
            ILogger<CategoryController> logger
        )
        {
            _commonService = staticDataCommonService;
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// List of <see cref="Category"/> categories
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="Category"/> category</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetAllCategories()
        {
            _logger.LogInformation($"{nameof(GetAllCategories)} - method start");
            BaseResponse<List<Category>> res = new();

            try
            {
                List<Category> categories = await _commonService.GetEntities();
                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetAllCategories)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetAllCategories)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// List of <see cref="Category"/> categories sorted by <see cref="CategoryType"/> type
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="Category"/> category</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetCategoriesByType(int category)
        {
            _logger.LogInformation($"{nameof(GetCategoriesByType)} - method start");
            BaseResponse<List<Category>> res = new();

            try
            {
                List<Category> categories = await _categoryService.GetCategoriesByCategoryType(category);
                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoriesByType)} - {res.Status} - {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoriesByType)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoriesByType)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// <see cref="Category"/> category
        /// </summary>
        /// <param name="req">Id of a <see cref="Category"/> category</param>
        /// <returns><see cref="Task"/> with <see cref="Category"/> category</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Category>> GetCategoryById(int id)
        {
            _logger.LogInformation($"{nameof(GetCategoryById)} - method start");
            BaseResponse<Category> res = new();

            try
            {
                Category category = await _commonService.GetEntity(id);
                res.Data = category;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.NOT_FOUND;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoryById)} - {res.Status} - {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoryById)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(GetCategoryById)} - {res.Status} - {ex.Message}");
            }

            return res;
        }
    }
}
