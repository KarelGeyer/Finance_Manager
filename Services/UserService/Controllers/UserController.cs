using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace UsersService.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Client _supabaseClient;

        public UserController(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _supabaseClient.From<User>().Get();
                var users = response.Models;

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
