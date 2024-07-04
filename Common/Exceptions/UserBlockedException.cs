using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
	public class UserBlockedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBlockedException"/> class.
		/// </summary>
		public UserBlockedException()
			: base(string.Format("User is blocked from attempting to login")) { }
	}
}
