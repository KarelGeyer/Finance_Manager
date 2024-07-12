using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.User
{
    public class UserGroup
    {
        public List<UserGroupSingleUser> Users { get; set; } = new List<UserGroupSingleUser>();
    }
}
