namespace Common.Models.PortfolioModels.Budget
{
	public class CreateBudget
	{
		/// <summary>
		/// An id representation of a <see cref="Budget"/>
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Represents a <see cref="Category.Category"/>'s Id
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Represents a <see cref="Budget"/>'s Value
		/// </summary>
		public float Value { get; set; }
	}
}
