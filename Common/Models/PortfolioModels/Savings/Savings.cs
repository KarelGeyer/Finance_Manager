using Common.Interfaces;
using Common.Models.PortfolioModels;

namespace Common.Models.Savings
{
    public class Savings : PortfolioModel, ISavings
    {
        /// <inheritdoc />
        public double Amount { get; set; }
    }
}
