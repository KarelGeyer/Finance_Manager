using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a user does not exist.
    /// </summary>
    public class UserDoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDoesNotExistException"/> class.
        /// </summary>
        public UserDoesNotExistException()
            : base(string.Format("User does not exist")) { }
    }
}
