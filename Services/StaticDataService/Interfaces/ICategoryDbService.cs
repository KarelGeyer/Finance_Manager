using Common.Models.Category;
using StaticDataService.Services;

namespace StaticDataService.Interfaces
{
	public interface ICategoryDbService
	{
		/// <summary>
		/// retrieves all all <see cref="Category."/> categories filtered by a <see cref="Category.CategoryTypeId"/>.
		/// </summary>
		/// <returns>List of entities</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		Task<List<Category>> GetCategoriesByCategoryTypesAsync(int category);
	}
}
