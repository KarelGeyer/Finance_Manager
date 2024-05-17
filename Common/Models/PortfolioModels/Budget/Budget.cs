using Common.Interfaces;

namespace Common.Models.PortfolioModels.Budget
{
	public class Budget : PortfolioModel, IBudget
	{
		/// <inheritdoc />
		public int Parent { get; set; }

		/// <inheritdoc />
		public int CategoryId { get; set; }

		/// <inheritdoc />
		public float Value { get; set; }
	}
}
