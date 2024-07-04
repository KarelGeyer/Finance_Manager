using Common.Models.User;
using Common.Exceptions;

namespace UserService.Interfaces
{
	public interface IUserService
	{
		/// <summary>
		/// Attempts to retrieve a user from database by id
		/// </summary>
		/// <param name="id">User's ID</param>
		/// <returns><see cref="User"/> user</returns>
		/// <exception cref="NotFoundException" />
		/// <exception cref="Exception" />
		Task<User> GetUser(int id);

		/// <summary>
		/// Attempts to retrieve a user from database by id
		/// </summary>
		/// <param name="username">username</param>
		/// <returns><see cref="User"/> user</returns>
		/// <exception cref="NotFoundException" />
		/// <exception cref="Exception" />
		Task<User> GetUser(string username);

		/// <summary>
		/// Attempts to update User's data
		/// </summary>
		/// <param name="updateUser">user to update</param>
		/// <returns>a bool value</returns>
		/// <exception cref="Exception" />
		/// <exception cref="NotFoundException" />
		/// <exception cref="FailedToUpdateException" />
		Task<bool> UpdateUser(UpdateUser updateUser);

		/// <summary>
		/// Attempts to delete user's account
		/// </summary>
		/// <param name="id">User's ID</param>
		/// <returns>a bool value</returns>
		/// <exception cref="Exception" />
		/// <exception cref="FailedToDeleteException" />
		Task<bool> DeleteUser(int id);

		/// <summary>
		/// Attempts to create a new user account
		/// </summary>
		/// <param name="newUser">new user</param>
		/// <returns>a bool value</returns>
		/// <exception cref="Exception" />
		/// <exception cref="FailedToCreateException" />
		Task<bool> CreateUser(CreateUser newUser);

		/// <summary>
		/// Attempts to change user's password
		/// </summary>
		/// <param name="updatePassword">update password</param>
		/// <returns>a bool value</returns>
		/// <exception cref="Exception" />
		/// <exception cref="NotFoundException" />
		/// <exception cref="FailedToUpdateException" />
		Task<bool> UpdatePassword(UpdatePassword updatePassword);

		/// <summary>
		/// Verify whetever user account has already been verified
		/// </summary>
		/// <param name="id">User's id</param>
		/// <returns>a bool value</returns>
		/// <exception cref="Exception" />
		/// <exception cref="NotFoundException" />
		/// <exception cref="FailedToUpdateException" />
		Task<bool> VerifyUser(int id);
	}
}
