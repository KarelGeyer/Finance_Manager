using Common.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<List<User>> GetUsers()
        {
            User user = new() { Id = 0, Name = "Test", };
            User user2 = new() { Id = 1, Name = "Test2", };
            User user3 = new() { Id = 2, Name = "Test3", };

            List<User> res = await Task.FromResult<List<User>>([user, user2, user3]);
            return res;
        }
    }
}
