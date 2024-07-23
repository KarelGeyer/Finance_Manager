using Common.Enums;
using Common.Exceptions;
using Common.Models.Response;
using Common.Models.User;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Interfaces.IAuthorizationService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(Interfaces.IAuthorizationService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        ///	Attempts to log in the user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<string>> Login([FromBody] Login login)
        {
            _logger.LogInformation($"{nameof(Login)} - method start");
            BaseResponse<string> response = new();

            try
            {
                string token = await _authService.Login(login.Username, login.Password);
                response.Data = token;
                response.Status = EHttpStatus.OK;
            }
            catch (NotFoundException ex)
            {
                response.Status = EHttpStatus.NOT_FOUND;
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(Login)} - {response.Status} - {ex.Message}");
            }
            catch (UserNotVerifiedException ex)
            {
                response.Status = EHttpStatus.FORBIDDEN;
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(Login)} - {response.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                response.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                response.ResponseMessage = ex.Message;
                _logger.LogError($"{nameof(Login)} - {response.Status} - {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Verifies whether passed token is correct
        /// </summary>
        /// <param name="token">token to be verified</param>
        [HttpGet]
        [Route("[action]")]
        public BaseResponse<bool> VerifyToken(string token)
        {
            _logger.LogInformation($"{nameof(VerifyToken)} - method start");
            BaseResponse<bool> response = new();

            VerifyTokenResponse result = _authService.VerifyToken(token);

            if (!result.IsCorrect)
                _logger.LogError($"{nameof(VerifyToken)} - Incorrect token");

            response.Data = result.IsCorrect;
            response.ResponseMessage = result.Message;
            response.Status = EHttpStatus.OK;

            return response;
        }
    }
}
