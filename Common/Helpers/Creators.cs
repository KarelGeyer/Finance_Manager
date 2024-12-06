using Common.Models.Email;
using Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class Creators
    {
        public static UserResponse GetUserResponse(User user)
        {
            return new UserResponse()
            {
                CurrencyId = user.CurrencyId,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                UserGroupId = (Guid)(user.UserGroupId)
            };
        }
    }
}
