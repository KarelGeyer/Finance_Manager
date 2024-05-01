namespace Common.Models.Income
{
    /// <summary>
    /// Represents an income.
    /// </summary>
    public class Income : BaseDbModel
    {
        /// <summary>
        /// Gets or sets the owner ID of the income.
        /// </summary>
        public int OwnerId { get; set; }

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

        /// <summary>
        /// Gets or sets the creation date of the income.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
