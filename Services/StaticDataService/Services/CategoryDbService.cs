using Common.Exceptions;
using Common.Models.Category;
using Microsoft.EntityFrameworkCore;
using StaticDataService.Db;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
	public class CategoryDbService : ICategoryDbService
	{
		private readonly DataContext _context;

		public CategoryDbService(DataContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<List<Category>> GetCategoriesByCategoryTypesAsync(int category)
		{
			List<Category> categories = await _context.Set<Category>().Where(x => x.CategoryTypeId == category).ToListAsync();
			return categories;
		}
	}
}
