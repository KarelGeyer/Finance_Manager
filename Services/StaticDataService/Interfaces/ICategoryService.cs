using Common.Models.Category;

namespace StaticDataService.Interfaces
{
	public interface ICategoryService
	{
		/// <summary>
		/// Calls the db service to attempt to retrieve all <see cref="Category."/> categories filtered by a <see cref="Category.CategoryTypeId"/>.
		/// </summary>
		/// <returns>List of entities</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<Category>> GetCategoriesByCategoryType(int category);
	}
}
