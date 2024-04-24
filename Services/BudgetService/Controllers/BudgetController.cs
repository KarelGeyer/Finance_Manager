using BudgetService.Db;
using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IDbService _dbService;

        public BudgetController(IDbService dbService)
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
                var categories = await _dbService.GetAllCategories();
                res.Data = categories;
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
    }
}
