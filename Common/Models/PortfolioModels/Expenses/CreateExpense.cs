namespace Common.Models.Expenses
{
    public class CreateExpense
    {
        /// <summary>
        /// Gets or sets the owner Id of the expense.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the expense's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the expense.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the expense.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
