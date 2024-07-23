using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.User
{
    public class UpdateUser
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Username { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public int CurrencyId { get; set; }
    }
}
