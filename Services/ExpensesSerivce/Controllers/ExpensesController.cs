using Common.Enums;
using Common.Models.Expenses;
using Common.Response;
using CurrencyService.Db;
using Microsoft.AspNetCore.Mvc;
using Postgrest.Responses;

namespace ExpensesSerivce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IDbService _dbService;

        public ExpensesController(ILogger<ExpensesController> logger, IDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Expense>>> GetAll(int ownerId)
        {
            BaseResponse<List<Expense>> res = new();

            if (ownerId == 0)
                throw new ArgumentNullException(nameof(ownerId));

            try
            {
                List<Expense> expenses = await _dbService.GetAll(ownerId);
                res.Data = expenses;
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
    }
}
