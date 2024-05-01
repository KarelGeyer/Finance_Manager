using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.User
{
    [Table("Users")]
    public class CreateUser
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Surname")]
        public string Surname { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("CurrencyId")]
        public int CurrencyId { get; set; }
    }
}
