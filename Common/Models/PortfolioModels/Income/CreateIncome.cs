using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ProductModels.Income
{
	/// <summary>
	/// Represents a request to create an income.
	/// </summary>
	public class CreateIncome
	{
		/// <summary>
		/// Gets or sets the owner ID.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public float Value { get; set; }

		/// <summary>
		/// Gets or sets the category ID.
		/// </summary>
		public int CategoryId { get; set; }
	}
}
