using Common.Exceptions;

namespace StaticDataService.Db
{
	public interface IDbService<T>
	{
		/// <summary>
		/// retrieves all <see cref="T"/> entities for a specific owner.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <returns>List of <see cref="T"/> entities</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<List<T>> GetAllAsync();

		/// <summary>
		/// retrieves a specific <see cref="T"/> entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>Entity</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<T> GetAsync(int id);
	}
}
