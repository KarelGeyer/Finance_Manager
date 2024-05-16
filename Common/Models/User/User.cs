using Postgrest.Attributes;
using Postgrest.Models;

namespace Common.Models.User
{
	[Table("Users")]
	public class User : BaseModel
	{
		[PrimaryKey("Id", false)]
		public int Id { get; set; }

		[Column("UserGroupId")]
		public int UserGroupId { get; set; }

		[Column("Name")]
		public string Name { get; set; }

		[Column("Username")]
		public string Username { get; set; }

		[Column("Surname")]
		public string Surname { get; set; }

		[Column("Email")]
		public string Email { get; set; }

		[Column("Password")]
		public string Password { get; set; }

		[Column("IsVerified")]
		public bool IsVerified { get; set; }

		[Column("CurrencyId")]
		public int CurrencyId { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
	}
}
