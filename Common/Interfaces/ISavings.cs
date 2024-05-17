using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
	public interface ISavings
	{
		/// <summary>
		/// Gets or sets the amount of the savings.
		/// </summary>
		double Amount { get; }

		/// <summary>
		/// Gets or sets the owner ID of the income.
		/// </summary>
		int OwnerId { get; }
	}
}
