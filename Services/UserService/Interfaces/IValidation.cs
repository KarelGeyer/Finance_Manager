using Common.Enums;
using Common.Models.User;
using Microsoft.AspNetCore.Identity;

namespace UserService.Interfaces
{
	public interface IValidation
	{
		public void ValidateCreateUser(CreateUser user);

		public void ValidatePasswordRequests(
			string currentPassword,
			string oldPassword,
			string data,
			IPasswordHasher<User> hasher,
			EAuthRequestType requestType
		);
	}
}
