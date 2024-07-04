using Common.Enums;
using Common.Exceptions;
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
		private readonly IDbService<UserAuth> _userAuthDbService;
		private readonly IValidation _validator;
		private readonly IJwt _jwt;

		public AuthorizationService(
			IPasswordHasher<User> passwordHasher,
			IDbService<User> dbService,
			IDbService<UserAuth> userAuthDbService,
			IValidation validator,
			IJwt jwt
		)
		{
			_passwordHasher = passwordHasher;
			_userDbService = dbService;
			_userAuthDbService = userAuthDbService;
			_validator = validator;
			_jwt = jwt;
		}

		/// <inheritdoc/>
		public async Task<string> Login(string username, string password)
		{
			_validator.ValidateString(username);
			_validator.ValidateString(password);

			try
			{
				User? user = await _userDbService.Get(x => x.Username == username);
				string token = string.Empty;

				if (user != null)
				{
					UserAuth? existingUserAuth = await _userAuthDbService.Get(x => x.UserId == user.Id);

					if (existingUserAuth != null && existingUserAuth.LoginCounter >= 3)
					{
						if (existingUserAuth.LoginCounter == 3)
						{
							// Todo: send email with username to allow unblocking the user account
						}
						throw new UserBlockedException();
					}

					if (!user.IsVerified)
						throw new UserNotVerifiedException();

					bool isValid = _validator.ValidatePasswordRequest(password, user.Password, _passwordHasher);
					if (!isValid)
					{
						IncorrectCredentialsException ex = new();
						if (existingUserAuth == null)
						{
							UserAuth userAuthToCreate =
								new()
								{
									UserId = user.Id,
									LoginCounter = 1,
									CreatedAt = DateTime.UtcNow,
								};
							await _userAuthDbService.Create(userAuthToCreate);
							ex.Data.Add("counter", userAuthToCreate.LoginCounter);
						}
						else
						{
							existingUserAuth.LoginCounter++;
							UserAuth? updatedUserAuth = await _userAuthDbService.Update(existingUserAuth);
							ex.Data.Add("counter", existingUserAuth.LoginCounter);
						}

						throw ex;
					}

					token = _jwt.CreateJWT(user);
				}
				else
					throw new IncorrectCredentialsException();

				return token;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <inheritdoc/>
		public VerifyTokenResponse VerifyToken(string token)
		{
			VerifyTokenResponse response = new();

			string errorMessage;
			bool result = _jwt.VerifyJWT(token, out errorMessage);

			response.IsCorrect = result;
			response.Message = errorMessage;

			return response;
		}
	}
}
