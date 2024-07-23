using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserBlockedException"/> class.
        /// </summary>
        public UserAlreadyExistsException()
            : base(string.Format("User with this username already exists")) { }
    }
}
