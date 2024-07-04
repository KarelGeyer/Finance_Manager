using Common.Models.User;
using System.IdentityModel.Tokens.Jwt;

namespace UserService.Interfaces
{
	public interface IJwt
	{
		/// <summary>
		/// Creates new jwt token for the user account
		/// </summary>
		/// <param name="user"><see cref="User"/> user</param>
		/// <returns>a token</returns>
		string CreateJWT(User user);

		/// <summary>
		/// Verifies whether token is correct
		/// </summary>
		/// <param name="token">a token to be verified</param>
		/// <param name="jwt">jwt security token</param>
		/// <param name="errorMessage">a message to show errors</param>
		/// <returns>a bool whether token is correct</returns>
		public bool VerifyJWT(string token, out string errorMessage);
	}
}
