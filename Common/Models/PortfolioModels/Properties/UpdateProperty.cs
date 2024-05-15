namespace Common.Models.PortfolioModels.Properties
{
    public class UpdateProperty
    {
        /// <summary>
        /// Gets or sets the Id of the property.
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        public decimal Value { get; set; }
    }
}
