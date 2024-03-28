using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDbService<UserModel> _dbService;

        public UserController(IDbService<UserModel> dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// List of <see cref="UserModel"/> users
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="List{T}"/> where T equals <see cref="UserModel"/> user</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<UserModel>>> GetAllAsync()
        {
            BaseResponse<List<UserModel>> res = new();

            try
            {
                var users = await _dbService.GetAllAsync();
                res.Data = users;
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
