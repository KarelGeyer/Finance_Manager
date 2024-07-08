using Common.Interfaces;
using Common.Models.Expenses;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Properties;

namespace Common.Models.PortfolioModels
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
    public class CommonPortfolioModel : PortfolioModel, ICommonPortfolioModel
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public double Value { get; set; }

        /// <inheritdoc />
        public int CategoryId { get; set; }
    }
}
