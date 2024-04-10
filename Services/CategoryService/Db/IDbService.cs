using Common.Enums;
using Common.Models;
using Common.Models.Category;

namespace CategoryService.Service
{
    public interface IDbService
    {
        /// <summary>
        /// Retrieves all categories from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        Task<List<Category>> GetAllCategories();

        /// <summary>
        /// Attempts to find a category using provided id
        /// </summary>
        /// <returns>Task with a category</returns>
        Task<Category> GetCategory(int id);

        /// <summary>
        /// Retrieves all categories of a given type from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        Task<List<Category>> GetCategoriesByCategoryType(int categoryTypeId);

        /// <summary>
        /// Retrieves all category types from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        Task<List<CategoryType>> GetAllCategoryTypes();

        /// <summary>
        /// Attempts to find a category type using provided id
        /// </summary>
        /// <returns>Task with a category</returns>
        Task<CategoryType> GetCategoryType(int id);
    }
}
