using Common.Models.Expenses;
using Common.Models.PortfolioModels;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Properties;

namespace Common.Interfaces
{
    public interface IPortfolioValidation<T>
        where T : PortfolioModel
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
        /// <exception cref="ArgumentException"></exception>
        void ValidatePortfolioModel(T model);
    }
}
