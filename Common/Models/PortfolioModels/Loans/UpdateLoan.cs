namespace Common.Models.ProductModels.Loans
{
	public class UpdateLoan
	{
		/// <summary>
		/// Gets or sets the id of the loan.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the value of the loan.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Gets or sets the name of the loan.
		/// </summary>
		public string Name { get; set; }
	}
}
