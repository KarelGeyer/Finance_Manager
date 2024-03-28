using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models.User;
using Postgrest.Attributes;

namespace Common.Models.Currency
{
    [Table("Currencies")]
    public class CurrencyModel : BaseDbModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        public virtual ICollection<UserModel> Users { get; set; } = new HashSet<UserModel>();
    }
}
