using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using DbService;
using Microsoft.AspNetCore.Identity;
using UserService.Interfaces;

namespace UserService.Services
{
	public class AuthorizationService
	{
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IDbService<User> _dbService;
		private readonly IValidation _validator;
		private readonly IJwt _jwt;

		public AuthorizationService(IPasswordHasher<User> passwordHasher, IDbService<User> dbService, IValidation validator, IJwt jwt)
		{
			_passwordHasher = passwordHasher;
			_dbService = dbService;
			_validator = validator;
			_jwt = jwt;
		}

		public async Task<string> Login(string username, string password)
		{
			User? user = await _dbService.Get(x => x.Username == username);
			if (user == null)
				throw new NotFoundException();

			_validator.ValidatePasswordRequests(password, user.Password, user.Username, _passwordHasher, EAuthRequestType.LOGIN);

			string token = _jwt.CreateJWT(user);
			return token;
		}
	}
}
