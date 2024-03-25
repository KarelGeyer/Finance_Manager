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
    [Route("api/categoryType")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly IDbService<CategoryType> _dbService;

        public CategoryTypeController(IDbService<CategoryType> dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// List of <see cref="CategoryType"/> category types
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="CategoryType"/> category type</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<CategoryType>>> GetAll()
        {
            BaseResponse<List<CategoryType>> res = new();

            try
            {
                var categoryTypes = await _dbService.GetAllAsync();
                res.Data = categoryTypes;
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
        /// <see cref="CategoryType"/> category type
        /// </summary>
        /// <param name="req">Id of a <see cref="CategoryType"/> category type</param>
        /// <returns><see cref="Task"/> with <see cref="CategoryType"/> category type</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<CategoryType>> GetById([FromBody] BaseRequest<int> req)
        {
            BaseResponse<CategoryType> res = new();

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
