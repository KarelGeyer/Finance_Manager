using Common.Enums;
using Common.Models;
using Common.Models.Category;

namespace CategoryService.Service
{
    public interface IDbService<T>
        where T : BaseDbModel, new()
    {
        /// <summary>
        /// Retrieves all categories from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Attempts to find a category using provided id
        /// </summary>
        /// <returns>Task with a category</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Retrieves all categories of a given type from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        Task<List<Category>> GetByCategoryAsync(int categoryTypeId);
    }
}
