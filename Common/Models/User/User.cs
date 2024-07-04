using Postgrest.Attributes;
using Postgrest.Models;

namespace Common.Models.User
{
	[Table("Users")]
	public class User : BaseDbModel
	{
		public int UserGroupId { get; set; }

		public string Name { get; set; }

		public string Username { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public int CurrencyId { get; set; }

		public bool IsVerified { get; set; }
	}
}
