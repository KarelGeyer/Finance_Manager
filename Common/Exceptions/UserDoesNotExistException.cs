using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException()
            : base(string.Format("User does not exist")) { }
    }
}
