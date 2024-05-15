namespace Common.Models.Expenses
{
    public class UpdateExpense
	{
        /// <summary>
        /// Gets or sets the Id of the expense.
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the expense.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the expense.
        /// </summary>
        public decimal Value { get; set; }
	}
}
