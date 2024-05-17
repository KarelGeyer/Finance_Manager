using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Response;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
	public class StaticDataCommonService<T> : IStaticDataCommonService<T>
	{
		private readonly IDbService<T> _dbService;
		private readonly ICategoryDbService _categoryDbService;

		public StaticDataCommonService(IDbService<T> dbService, ICategoryDbService categoryDbService)
		{
			_dbService = dbService;
			_categoryDbService = categoryDbService;
		}

		/// <inheritdoc />
		public async Task<List<T>> GetEntities()
		{
			try
			{
				List<T> list = await _dbService.GetAllAsync();
				if (list.Count.Equals(0))
					throw new NotFoundException();
				return list;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<T> GetEntity(int id)
		{
			if (id == 0 || id < 0)
				throw new ArgumentNullException("id");

			try
			{
				T? entity = await _dbService.GetAsync(id);
				if (entity == null)
					throw new NotFoundException(id);
				return entity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<Category>> GetCategoriesByCategoryType(int category)
		{
			if (id == 0 || id < 0)
				throw new ArgumentNullException("category");

			try
			{
				List<Category> categories = await _categoryDbService.GetCategoriesByCategoryTypesAsync(category);
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
