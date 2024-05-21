using Common.Models.Expenses;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Properties;

namespace Common.Interfaces
{
	/// <summary>
	/// An interface for a common portfolio model.
	/// Common portoflio model includes:
	/// <list type="bullet">
	/// <item><see cref="Expense"/></item>
	/// <item><see cref="Income"/></item>
	/// <item><see cref="Property"/></item>
	/// </list>
	/// </summary>
	public interface ICommonPortfolioModel : IBaseDbModel
	{
		/// <summary>
		/// Gets or sets the name of the property.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the value of the property.
		/// </summary>
		double Value { get; }

		/// <summary>
		/// Gets or sets the category ID of the property.
		/// </summary>
		int CategoryId { get; }

		/// <summary>
		/// Gets or sets the owner ID of the income.
		/// </summary>
		int OwnerId { get; set; }
	}
}
