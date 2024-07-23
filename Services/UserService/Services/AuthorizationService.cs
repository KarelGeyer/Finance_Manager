using Common.Enums;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models.Response;
using Common.Models.User;
using DbService;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using UserService.Interfaces;

namespace UserService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDbService<User> _userDbService;
        private readonly IUserValidation _validator;
        private readonly IJwt _jwt;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(
            IPasswordHasher<User> passwordHasher,
            IDbService<User> dbService,
            IUserValidation validator,
            IJwt jwt,
            ILogger<Services.AuthorizationService> logger
        )
        {
            _passwordHasher = passwordHasher;
            _userDbService = dbService;
            _validator = validator;
            _jwt = jwt;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<string> Login(string username, string password)
        {
            _logger.LogInformation($"{nameof(Login)} - method start");

            try
            {
                _validator.ValidateString(username);
                _validator.ValidateString(password);

                User? user = await _userDbService.Get(x => x.Username == username);
                string token = string.Empty;

                if (user != null)
                {
                    if (user.IsBlocked)
                    {
                        // Todo: send email with username to allow unblocking the user account
                        _logger.LogError($"{nameof(Login)} - blocked user with id {user.Id} did attempt to log in");
                        throw new UserBlockedException();
                    }

                    if (!user.IsVerified)
                    {
                        _logger.LogError($"{nameof(Login)} - unverified user with id {user.Id} did attempt to log in");
                        throw new UserNotVerifiedException();
                    }

                    bool isValid = _validator.ValidatePasswordRequest(user.Password, password, _passwordHasher, user);
                    if (!isValid)
                    {
                        IncorrectCredentialsException ex = new();
                        if (user.LoginCounter < 3)
                        {
                            user.LoginCounter++;
                            _logger.LogWarning($"{nameof(Login)} - increasing users failed login count to: {user.LoginCounter}");
                        }
                        else
                        {
                            _logger.LogWarning($"{nameof(Login)} - blocking user with id: {user.Id}");
                            user.IsBlocked = true;
                        }

                        await _userDbService.Update(user);
                        ex.Data.Add("counter", user.LoginCounter);
                        _logger.LogError($"{nameof(Login)} - {ex.Message}");
                        throw ex;
                    }

                    token = _jwt.CreateJWT(user);
                }
                else
                {
                    IncorrectCredentialsException ex = new();
                    _logger.LogError($"{nameof(Login)} - {ex.Message}");
                    throw ex;
                }

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Login)} - {ex.Message}");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VerifyTokenResponse VerifyToken(string token)
        {
            _logger.LogInformation($"{nameof(VerifyToken)} - method start");
            VerifyTokenResponse response = new();

            string errorMessage;
            bool result = _jwt.VerifyJWT(token, out errorMessage);

            if (!result)
                _logger.LogError($"{nameof(VerifyToken)} - Incorrect token");

            response.IsCorrect = result;
            response.Message = errorMessage;

            return response;
        }
    }
}
