using Common.Exceptions;

namespace PortfolioService.Db
{
	public interface IDbService<T>
	{
		/// <summary>
		/// retrieves all <see cref="T"/> entities for a specific owner.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <returns>List of <see cref="T"/> entities</returns>
		Task<List<T>> GetAllAsync(int ownerId);

		/// <summary>
		/// retrieves all <see cref="T"/> entities for a specific owner, month and year.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <param name="month">CreatedAt Month</param>
		/// <param name="year">CreatedAt Year </param>
		/// <returns>List of <see cref="T"/> entities</returns>
		Task<List<T>> GetAllAsync(int ownerId, int month, int year);

		/// <summary>
		/// retrieves a specific <see cref="T"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>Entity</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<T> GetAsync(int id);

		/// <summary>
		/// Creates a new <see cref="T"/> entity.
		/// </summary>
		/// <param name="entity">Entity to be created</param>
		/// <returns>true if created, else false</returns>
		/// <exception cref="FailedToCreateException{T}"></exception>
		Task<bool> CreateAsync(T entity);

		/// <summary>
		/// Updates a <see cref="T"/> entity.
		/// </summary>
		/// <param name="entity">Updated entity</param>
		/// <returns>true if updated, else false</returns>
		/// <exception cref="FailedToUpdateException{T}"></exception>
		Task<bool> UpdateAsync(T entity);

		/// <summary>
		/// Deletes a <see cref="T"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>true if deleted, else false</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="FailedToDeleteException{T}"></exception>
		Task<bool> DeleteAsync(int id);
	}
}
