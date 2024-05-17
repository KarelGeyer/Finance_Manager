using Common.Interfaces;
using Common.Models.PortfolioModels;

namespace Common.Models.ProductModels.Properties
{
	public class Property : PortfolioModel, ICommonPortfolioModel
	{
		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public double Value { get; set; }

		/// <inheritdoc />
		public int CategoryId { get; set; }
	}
}
