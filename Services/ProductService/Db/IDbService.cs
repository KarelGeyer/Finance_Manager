using Common.Models.Properties;

namespace ProductService.Db
{
    public interface IDbService<T>
    {
        Task<List<T>> GetAllAsync(int ownerId, int month);

        Task<T> GetAsync(int id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
    }
}
