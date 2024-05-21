using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
	public interface IBaseDbModel
	{
		/// <summary>
		/// Get or sets the entity Id attribute
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the creation date of the income.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }
	}
}
