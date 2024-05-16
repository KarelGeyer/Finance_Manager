using Common.Models.PortfolioModels;

namespace Common.Models.Expenses
{
	public class Expense : PortfolioModel
	{
		/// <summary>
		/// Gets or sets the name of the expense.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value of the expense.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Gets or sets the category ID of the expense.
		/// </summary>
		public int CategoryId { get; set; }
	}
}
