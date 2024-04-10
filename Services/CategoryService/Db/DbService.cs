using CategoryService.Db;
using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Category;
using Microsoft.EntityFrameworkCore;
using Supabase;

namespace CategoryService.Service
{
    public class DbService : IDbService
    {
        private readonly DataContext _dataContext;

        public DbService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categories = await _dataContext.Categories.ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                throw new NotFoundException();
            }

            return categories;
        }

        /// <inheritdoc/>
        public async Task<Category> GetCategory(int id)
        {
            Category category = await _dataContext.Categories.Where(x => x.Id == id).FirstAsync();

            if (category == null)
            {
                throw new NotFoundException(id);
            }

            return category;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetCategoriesByCategoryType(int categoryTypeId)
        {
            List<Category> categories = await _dataContext.Categories
                .Where(x => x.CategoryTypeId == categoryTypeId)
                .ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                throw new NotFoundException();
            }

            return categories;
        }

        /// <inheritdoc/>
        public async Task<List<CategoryType>> GetAllCategoryTypes()
        {
            List<CategoryType> categorytypes = await _dataContext.CategoriesTypes.ToListAsync();

            if (categorytypes == null || categorytypes.Count == 0)
            {
                throw new NotFoundException();
            }

            return categorytypes;
        }

        /// <inheritdoc/>
        public async Task<CategoryType> GetCategoryType(int id)
        {
            CategoryType categoryType = await _dataContext.CategoriesTypes
                .Where(x => x.Id == id)
                .SingleAsync();

            if (categoryType == null)
            {
                throw new NotFoundException(id);
            }

            return categoryType;
        }
    }
}
