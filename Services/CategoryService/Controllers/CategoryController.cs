using CategoryService.Service;
using Common;
using Common.Category;
using Common.Enums;
using Common.Exceptions;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IDbService<Category> _dbService;

        public CategoryController(IDbService<Category> dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// List of <see cref="Category"/> categories
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="Category"/> category</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetAll()
        {
            BaseResponse<List<Category>> res = new();

            try
            {
                var categories = await _dbService.GetAllAsync();
                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// List of <see cref="Category"/> categories sorted by <see cref="CategoryType"/> type
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="Category"/> category</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetByType([FromBody] BaseRequest<int> req)
        {
            BaseResponse<List<Category>> res = new();

            try
            {
                var categories = await _dbService.GetByCategoryAsync(req.Data);
                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
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
        public async Task<BaseResponse<Category>> GetById([FromBody] BaseRequest<int> req)
        {
            BaseResponse<Category> res = new();

            try
            {
                var category = await _dbService.GetAsync(req.Data);
                res.Data = category;
                res.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }
    }
}
