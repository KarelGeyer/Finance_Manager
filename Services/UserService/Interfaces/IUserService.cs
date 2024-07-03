using Common.Models.User;

namespace UserService.Interfaces
{
	public interface IUserService
	{
		Task<User> GetUser(int id);
		Task<User> GetUser(string username);
		Task<bool> UpdateUser(UpdateUser updateUser);
		Task<bool> DeleteUser(int id);
		Task<bool> CreateUser(CreateUser newUser);
		Task<bool> UpdatePassword(UpdatePassword updatePassword);
		Task<bool> VerifyUser(int id);
	}
}
