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
		/// Creates a new <see cref="PortfolioModel"/> entity.
		/// </summary>
		/// <param name="entity">Entity to be created</param>
		/// <returns>true if created, else false</returns>
		/// <exception cref="FailedToCreateException{T}"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		Task<bool> CreateEntity(T entity);

		/// <summary>
		/// Updates a <see cref="PortfolioModel"/> entity.
		/// </summary>
		/// <param name="entity">Request data</param>
		/// <returns>true if updated, else false</returns>
		/// <exception cref="FailedToUpdateException{T}"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		Task<bool> UpdateEntity(T entity);

		/// <summary>
		/// Deletes a <see cref="PortfolioModel"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>true if deleted, else false</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="FailedToDeleteException{T}"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<bool> DeleteEntity(int id);

		/// <summary>
		/// Calls db service to retrieve a specific <see cref="PortfolioModel"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns><see cref="PortfolioModel"/> Entity</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<T> GetEntity(int id);

		/// <summary>
		/// Calls common db service to retrieve all <see cref="PortfolioModel"/> entities for a specific owner.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <returns>List of <see cref="PortfolioModel"/> entities</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetEntities(int ownerId);

		/// <summary>
		/// Calls common db service retrieves all <see cref="PortfolioModel"/> entities for a specific owner, month and year.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <param name="month">CreatedAt Month</param>
		/// <param name="year">CreatedAt Year </param>
		/// <returns>List of <see cref="PortfolioModel"/> entities</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetEntities(int ownerId, int month, int year);

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
		Task<List<P>> GetCommonPortfolioEntitiesSortedByGivenParameter<P>(int ownerId, bool shouldBeInReversedOrder, EPortfolioModelSortBy parameter)
			where P : CommonPortfolioModel;

		/// <summary>
		/// Calls common db service to retrieve all entities for a specific owner and a category.
		/// </summary>
		/// <typeparam name="P"><see cref="CommonPortfolioModel"/></typeparam>
		/// <param name="ownerId">User Id</param>
		/// <param name="categoryId">Category Id</param>
		/// <returns>A list of <see cref="CommonPortfolioModel"/> for a given category</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<P>> GetCommonPortfolioEntitiesByCategory<P>(int ownerId, int categoryId)
			where P : CommonPortfolioModel;
	}
}
