using Common.Interfaces;
using Common.Models.PortfolioModels;

namespace Common.Models.ProductModels.Loans
{
    public class Loan : PortfolioModel, ILoan
    {
        /// <inheritdoc />
        public int ToPerson { get; set; }

        /// <inheritdoc />
        public double Value { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
    }
}
