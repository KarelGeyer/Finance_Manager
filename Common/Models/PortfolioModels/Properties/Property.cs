using Common.Models.PortfolioModels;

namespace Common.Models.ProductModels.Properties
{
	public class Property : PortfolioModel
	{
		/// <summary>
		/// Gets or sets the name of the property.
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
