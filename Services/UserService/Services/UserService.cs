using Common.Enums;
using Common.Exceptions;
using Common.Models.User;
using DbService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserService.Interfaces;

namespace UserService.Services
{
	[Authorize]
	public class UserService : IUserService
	{
		private readonly IDbService<User> _dbService;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IValidation _validator;

		public UserService(IDbService<User> dbService, IPasswordHasher<User> passwordHasher, IValidation validator)
		{
			_dbService = dbService;
			_passwordHasher = passwordHasher;
			_validator = validator;
		}

		public async Task<User> GetUser(int id)
		{
			try
			{
				User? user = await _dbService.Get(id);
				if (user == null)
					throw new NotFoundException();
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<User> GetUser(string username)
		{
			try
			{
				User? user = await _dbService.Get(x => x.Username == username);
				if (user == null)
					throw new NotFoundException();
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateUser(UpdateUser updateUser)
		{
			try
			{
				User? user = await _dbService.Get(updateUser.Id);
				if (user == null)
					throw new NotFoundException();

				if (!string.IsNullOrWhiteSpace(updateUser.Name))
					user.Name = updateUser.Name;
				if (!string.IsNullOrWhiteSpace(updateUser.Surname))
					user.Surname = updateUser.Surname;
				if (!string.IsNullOrWhiteSpace(updateUser.Email))
					user.Email = updateUser.Email;
				if (!string.IsNullOrWhiteSpace(updateUser.Username))
					user.Username = updateUser.Username;
				if (updateUser.CurrencyId == 0)
					user.CurrencyId = updateUser.CurrencyId;

				User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
				if (updatedUser == null)
					throw new FailedToUpdateException<User>(user.Id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdatePassword(UpdatePassword updatePassword)
		{
			try
			{
				User? user = await _dbService.Get(updatePassword.Id);
				if (user == null)
					throw new NotFoundException();

				_validator.ValidatePasswordRequests(
					user.Password,
					updatePassword.OldPassword,
					updatePassword.NewPassword,
					_passwordHasher,
					EAuthRequestType.UPDATE_PASSWORD
				);

				string password = _passwordHasher.HashPassword(user, updatePassword.NewPassword);
				user.Password = password;

				User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
				if (updatedUser == null)
					throw new FailedToUpdateException<User>(user.Id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> DeleteUser(int id)
		{
			try
			{
				User? deletedUser = await _dbService.Delete(x => x.Id == id);
				if (deletedUser == null)
					throw new FailedToDeleteException<User>(id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> CreateUser(CreateUser newUser)
		{
			_validator.ValidateCreateUser(newUser);
			string password = _passwordHasher.HashPassword(null, newUser.Password);

			User userToCreate =
				new()
				{
					Name = newUser.Name,
					Surname = newUser.Surname,
					Email = newUser.Email,
					Username = newUser.Username,
					Password = password,
					CurrencyId = newUser.CurrencyId,
					IsVerified = false,
					CreatedAt = DateTime.Now
				};

			try
			{
				int user = await _dbService.Create(userToCreate);
				if (user == 0)
					throw new FailedToCreateException<User>();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> VerifyUser(int id)
		{
			try
			{
				User? user = await _dbService.Get(id);
				if (user == null)
					throw new NotFoundException();

				user.IsVerified = true;

				User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
				if (updatedUser == null)
					throw new FailedToUpdateException<User>(user.Id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
