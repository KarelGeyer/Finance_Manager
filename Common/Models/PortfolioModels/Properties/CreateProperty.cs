namespace Common.Models.ProductModels.Properties
{
	public class CreateProperty
	{
		/// <summary>
		/// Gets or sets the owner Id of the property.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Gets or sets the property's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value of the property.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Gets or sets the category ID of the property.
		/// </summary>
		public int CategoryId { get; set; }
	}
}
