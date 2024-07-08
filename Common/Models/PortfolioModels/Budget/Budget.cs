using Common.Interfaces;

namespace Common.Models.PortfolioModels.Budget
{
    public class Budget : PortfolioModel, IBudget
    {
        /// <inheritdoc />
        public int CategoryId { get; set; }

        /// <inheritdoc />
        public double Value { get; set; }
    }
}
