using Common.Models.PortfolioModels;

namespace Common.Models.ProductModels.Income
{
    /// <summary>
    /// Represents an income.
    /// </summary>
    public class Income : PortfolioModel
    {
        /// <summary>
        /// Gets or sets the name of the income.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the income.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the income.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
