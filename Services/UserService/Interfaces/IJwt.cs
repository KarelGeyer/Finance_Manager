using Common.Models.User;

namespace UserService.Interfaces
{
	public interface IJwt
	{
		string CreateJWT(User user);
	}
}
