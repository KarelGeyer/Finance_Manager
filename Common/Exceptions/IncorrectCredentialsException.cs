using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
	public class IncorrectCredentialsException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IncorrectCredentialsException"/> class.
		/// </summary>
		public IncorrectCredentialsException()
			: base(string.Format("Incorrect username or password")) { }
	}
}
