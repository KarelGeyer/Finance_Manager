using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.User
{
	public class UpdatePassword
	{
		public int Id { get; set; }

		public string OldPassword { get; set; }

		public string NewPassword { get; set; }
	}
}
