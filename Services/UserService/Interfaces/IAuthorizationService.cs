using Common.Models.Response;
using Common.Exceptions;

namespace UserService.Interfaces
{
	public interface IAuthorizationService
	{
		/// <summary>
		///	Attempts to log in the user
		/// </summary>
		/// <param name="username">username</param>
		/// <param name="password">password</param>
		/// <returns>a token</returns>
		/// <exception cref="UserBlockedException"
		/// <exception cref="UserNotVerifiedException"
		/// <exception cref="IncorrectCredentialsException"
		/// <exception cref="Exception"
		public Task<string> Login(string username, string password);

		/// <summary>
		/// Verifies whether passed token is correct
		/// </summary>
		/// <param name="token">token to be verified</param>
		/// <returns><see cref="VerifyTokenResponse"/> data</returns>
		public VerifyTokenResponse VerifyToken(string token);
	}
}
