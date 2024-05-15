using Common.Models.PortfolioModels;

namespace Common.Models.ProductModels.Loans
{
    public class Loan : PortfolioModel
    {
        /// <summary>
        /// Gets or sets the Id of a user who this is owned to of the loan.
        /// 0 means the user is not in the system
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// Gets or sets the value of the loan.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the loan.
        /// </summary>
        public string Name { get; set; }
    }
}
