using Common.Exceptions;
using Common.Models.Category;

namespace StaticDataService.Interfaces
{
	public interface ICommonService<T>
	{
		/// <summary>
		/// Calls the db service to attempt to retrieve all entities.
		/// </summary>
		/// <returns>List of entities</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetEntities();

		/// <summary>
		/// Calls the db service to attempt to retrieve a specific entity.
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <returns>Entity</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		Task<T> GetEntity(int id);
	}
}
