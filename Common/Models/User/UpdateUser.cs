using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.User
{
    public class UpdateUser : BaseDbModel
    {
        [Column("Username")]
        public string Username { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("CurrencyId")]
        public int CurrencyId { get; set; }
    }

    public class UpdateUserPassword : BaseDbModel
    {
        [Column("Password")]
        public string Password { get; set; }

        [Column("Password")]
        public string NewPassword { get; set; }
    }
}
