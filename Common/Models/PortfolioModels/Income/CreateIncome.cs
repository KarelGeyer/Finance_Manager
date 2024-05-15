namespace Common.Models.ProductModels.Income
{
    public class CreateIncome
	{
		/// <summary>
		/// Gets or sets the owner ID.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Gets or sets the name of the income.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value of the income.
		/// </summary>
		public float Value { get; set; }

		/// <summary>
		/// Gets or sets the category ID for the income.
		/// </summary>
		public int CategoryId { get; set; }
	}
}
