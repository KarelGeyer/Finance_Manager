using Common.Exceptions;
using Common.Models.Category;
using DbService;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IDbService<Category> _dbService;

		public CategoryService(IDbService<Category> dbService)
		{
			_dbService = dbService;
		}

		/// <inheritdoc />
		public async Task<List<Category>> GetCategoriesByCategoryType(int categoryTypeId)
		{
			if (categoryTypeId == 0 || categoryTypeId < 0)
				throw new ArgumentNullException("categoryTypeId");

			try
			{
				List<Category> categories = await _dbService.GetAll(x => x.CategoryTypeId == categoryTypeId);
				if (categories.Count.Equals(0))
					throw new NotFoundException();
				return categories;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
