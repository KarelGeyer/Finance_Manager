using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgrest.Attributes;

namespace Common.Models.User
{
    [Table("UsersGroups")]
    public class UserGroupModel : BaseDbModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("AmountOfUsers")]
        public int AmountOfUsers { get; set; }

        public virtual ICollection<UserModel> Users { get; set; } = new HashSet<UserModel>();
    }
}
