namespace PortfolioService.Interfaces
{
	public interface IDbService<T>
	{
		/// <summary>
		/// retrieves a specific <see cref="T"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>Entity</returns>
		/// <exception cref="Exception"></exception>
		Task<T?> GetAsync(int id);

		/// <summary>
		/// Creates a new <see cref="T"/> entity.
		/// </summary>
		/// <param name="entity">Entity to be created</param>
		/// <returns>true if created, else false</returns>
		/// <exception cref="Exception"></exception>
		Task<int> CreateAsync(T entity);

		/// <summary>
		/// Updates a <see cref="T"/> entity.
		/// </summary>
		/// <param name="updatedEntity">Entity with new values</param>
		/// <param name="entityToUpdate">Entity to be updated</param>
		/// <returns>true if updated, else false</returns>
		/// <exception cref="Exception"></exception>
		Task<int> UpdateAsync(T updatedEntity, T entityToUpdate);

		/// <summary>
		/// Deletes a <see cref="T"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>true if deleted, else false</returns>
		/// <exception cref="Exception"></exception>
		Task<int> DeleteAsync(T entity);
	}
}
