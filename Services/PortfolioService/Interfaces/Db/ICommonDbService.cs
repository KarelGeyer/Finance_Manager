using Common.Interfaces;
using Common.Models.PortfolioModels;

namespace PortfolioService.Interfaces.Db
{
	public interface ICommonDbService<T> : IDbService<T>
		where T : PortfolioModel
	{
		/// <summary>
		/// retrieves all entities for a specific owner, month and year.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <param name="month">CreatedAt Month</param>
		/// <param name="year">CreatedAt Year </param>
		/// <returns>List of <see cref="PortfolioModel"/> entities filtered by date</returns>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetAllByDateAsync(int ownerId, int month, int year);

		/// <summary>
		/// retrieves all entities for a specific owner.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <returns>List of <see cref="PortfolioModel"/> entities fitlered by their owner</returns>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetAllByOwnerAsync(int ownerId);

		/// <summary>
		/// retrieves all entities for a specific owner sorted by date.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">user Id</param>
		/// <param name="isReversedOrder">Wheter the list should be in reversed order or not</param>
		/// <returns>List of <see cref="CommonPortfolioModel"/> entities fitlered by their owner</returns>
		/// <exception cref="Exception"></exception>
		Task<List<P>> GetAllByOwnerSortedByDateAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel;

		/// <summary>
		/// retrieves all entities for a specific owner sorted by name.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">user Id</param>
		/// <param name="isReversedOrder">Wheter the list should be in reversed order or not</param>
		/// <returns>List of <see cref="CommonPortfolioModel"/> entities fitlered by their owner</returns>
		/// <exception cref="Exception"></exception>
		Task<List<P>> GetAllByOwnerSortedByNameAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel;

		/// <summary>
		/// retrieves all entities for a specific owner sorted by value.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">user Id</param>
		/// <param name="isReversedOrder">Wheter the list should be in reversed order or not</param>
		/// <returns>List of <see cref="CommonPortfolioModel"/> entities fitlered by their owner</returns>
		/// <exception cref="Exception"></exception>
		Task<List<P>> GetAllByOwnerSortedByValueAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel;

		/// <summary>
		/// Retrieves all entities for a specific owner and a category.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">User Id</param>
		/// <param name="categoryId"> Category Id</param>
		/// <returns>List of <see cref="CommonPortfolioModel"/> entities for a given category</returns>
		/// <exception cref="Exception"></exception>
		Task<List<P>> GetAllByCategory<P>(int ownerId, int categoryId)
			where P : CommonPortfolioModel;
	}
}
