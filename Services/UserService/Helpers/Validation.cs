using Common.Enums;
using Common.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using UserService.Interfaces;

namespace PortfolioService.Helpers
{
	public class Validation : IValidation
	{
		/// <inheritdoc/>
		public void ValidateCreateUser(CreateUser user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));
			if (string.IsNullOrWhiteSpace(user.Name))
				throw new ArgumentException("First name is required", nameof(user.Name));
			if (string.IsNullOrWhiteSpace(user.Surname))
				throw new ArgumentException("Last name is required", nameof(user.Surname));
			if (string.IsNullOrWhiteSpace(user.Username))
				throw new ArgumentException("Username is required", nameof(user.Username));
			if (string.IsNullOrWhiteSpace(user.Email))
				throw new ArgumentException("Email is required", nameof(user.Email));
			if (string.IsNullOrWhiteSpace(user.Password))
				throw new ArgumentException("Password is required", nameof(user.Password));
		}

		/// <inheritdoc/>
		public void ValidatePasswordRequest(
			string currentPassword,
			string oldPassword,
			string data,
			IPasswordHasher<User> hasher,
			EAuthRequestType requestType
		)
		{
			string dateMessageAttributeName = requestType == EAuthRequestType.UPDATE_PASSWORD ? "New password" : "Username";

			if (string.IsNullOrWhiteSpace(oldPassword))
				throw new ArgumentException("Old password is required", nameof(oldPassword));
			if (string.IsNullOrWhiteSpace(data))
				throw new ArgumentException($"{dateMessageAttributeName} is required", nameof(dateMessageAttributeName));

			PasswordVerificationResult isOldPasswordValid = hasher.VerifyHashedPassword(null, currentPassword, oldPassword);

			if (isOldPasswordValid == PasswordVerificationResult.Failed)
				throw new ArgumentException("Old password is incorrect", nameof(oldPassword));
		}

		/// <inheritdoc/>
		public bool ValidatePasswordRequest(string currentPassword, string oldPassword, IPasswordHasher<User> hasher)
		{
			PasswordVerificationResult isOldPasswordValid = hasher.VerifyHashedPassword(null, currentPassword, oldPassword);

			if (isOldPasswordValid == PasswordVerificationResult.Failed)
				return false;

			return true;
		}

		/// <inheritdoc/>
		public void ValidateString(string attribute)
		{
			if (!string.IsNullOrWhiteSpace(attribute))
				throw new ArgumentException(attribute, nameof(attribute));
		}
	}
}
