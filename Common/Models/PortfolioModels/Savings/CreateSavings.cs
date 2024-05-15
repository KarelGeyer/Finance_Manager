namespace Common.Models.Savings
{
    public class CreateSavings
	{
        /// <summary>
        /// Gets or sets the owner ID of the savings.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the amount of the savings.
        /// </summary>
		public float Amount { get; set; }
	}
}
