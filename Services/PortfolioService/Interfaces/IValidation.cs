using Common.Models.Expenses;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Properties;

namespace PortfolioService.Interfaces
{
	public interface IValidation<T>
	{
		/// <summary>
		/// Validate any of the following portfolio models:
		/// <list>
		/// <item><see cref="Expense"/> expense</item>
		/// <item><see cref="Property"/> property</item>
		/// <item><see cref="Income"/> income</item>
		/// </list>
		/// </summary>
		/// <param name="model"></param>
		void ValidatePortfolioModel(T model);
	}
}
