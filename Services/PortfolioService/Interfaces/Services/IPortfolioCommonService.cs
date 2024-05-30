using Common.Enums;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using Common.Models.PortfolioModels;

namespace PortfolioService.Interfaces.Services
{
	/// <summary>
	/// Represents a layer of logic between a controller and a basic database crud operations
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPortfolioCommonService<T>
		where T : PortfolioModel
	{
		/// <summary>
		/// Calls common db service to retrieve all entities for a specific owner sorted by <paramref name="parameter"/>
		/// and in order specified by <paramref name="shouldBeInReversedOrder"/>.
		/// Method can be used with <see cref="CommonPortfolioModel"/> entities.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">user Id</param>
		/// <param name="shouldBeInReversedOrder">wheter entities shoule be in reverse order or not</param>
		/// <param name="parameter">specifiy a property to sort by, can be: name, value or date</param>
		/// <returns>List of <see cref="CommonPortfolioModel"/> entities</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		List<T> GetCommonPortfolioEntitiesSortedByGivenParameter(int ownerId, bool shouldBeInReversedOrder, EPortfolioModelSortBy parameter);

		/// <summary>
		/// Calls common db service to retrieve all entities for a specific owner and a category.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">User Id</param>
		/// <param name="categoryId">Category Id</param>
		/// <returns>A list of <see cref="CommonPortfolioModel"/> for a given category</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetCommonPortfolioEntitiesByCategory(int ownerId, int categoryId);
	}
}
