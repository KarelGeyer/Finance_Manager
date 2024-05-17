namespace PortfolioService.Interfaces
{
	public interface ICommonDbService<T>
	{
		/// <summary>
		/// retrieves all entities for a specific owner, month and year.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <param name="month">CreatedAt Month</param>
		/// <param name="year">CreatedAt Year </param>
		/// <returns>List of entities filtered by date</returns>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetAllByDateAsync(int ownerId, int month, int year);

		/// <summary>
		/// retrieves all entities for a specific owner.
		/// </summary>
		/// <param name="ownerId">user Id</param>
		/// <returns>List of entities fitlered by their owner</returns>
		/// <exception cref="Exception"></exception>
		Task<List<T>> GetAllByOwnerAsync(int ownerId);
	}
}
