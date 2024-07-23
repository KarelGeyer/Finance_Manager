using Common.Exceptions;
using Common.Models.Category;
using DbService;
using StaticDataService.Controllers;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbService<Category> _dbService;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IDbService<Category> dbService, ILogger<CategoryService> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetCategoriesByCategoryType(int categoryTypeId)
        {
            _logger.LogInformation($"{nameof(GetCategoriesByCategoryType)} - method start");
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
                _logger.LogError($"{nameof(GetCategoriesByCategoryType)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
