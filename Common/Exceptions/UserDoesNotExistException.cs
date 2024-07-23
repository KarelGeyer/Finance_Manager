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
    public class UserNotVerifiedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotVerifiedException"/> class.
        /// </summary>
        public UserNotVerifiedException()
            : base(string.Format("User is not verified")) { }
    }
}
