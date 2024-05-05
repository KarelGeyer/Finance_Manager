using Common.Models.Properties;

namespace PortfolioService.Db
{
	public interface IDbService<T>
	{
		Task<List<T>> GetAllAsync(int ownerId);

		Task<List<T>> GetAllAsync(int ownerId, int month, int year);

		Task<T> GetAsync(int id);

		Task<bool> CreateAsync(T entity);

		Task<bool> UpdateAsync(T entity);

		Task<bool> DeleteAsync(int id);
	}
}
