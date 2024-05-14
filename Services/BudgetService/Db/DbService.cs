using CategoryService.Db;
using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Budget;
using Common.Models.Category;
using Common.Models.Income;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Db
{
    public class DbService : IDbService
    {
        private readonly DataContext _dataContext;

        public DbService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateBudget(Budget budget)
        {
            await _dataContext.Budgets.AddAsync(budget);
            int result = await _dataContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<Budget>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateBudgetOverview(BudgetOverviewRequest budgetOverview)
        {
            BudgetOverview newBudgetOverview =
                new() { For = DateTime.Now, OwnerId = budgetOverview.OwnerId, };

            await _dataContext.BudgetOverviews.AddAsync(newBudgetOverview);
            int result = await _dataContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<BudgetOverview>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBudget(int id, int parent)
        {
            Budget budget = await _dataContext.Budgets
                .Where(x => x.Parent == parent && x.Id == id)
                .SingleAsync();

            if (budget == null)
            {
                throw new NotFoundException();
            }

            _dataContext.Budgets.Remove(budget);
            int result = await _dataContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToDeleteException<Budget>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBudgetOverview(int userId, int budgetOverviewId)
        {
            BudgetOverview budgetOverview = await _dataContext.BudgetOverviews
                .Where(x => x.OwnerId == userId && x.Id == budgetOverviewId)
                .SingleAsync();

            if (budgetOverview == null)
            {
                throw new NotFoundException();
            }

            _dataContext.BudgetOverviews.Remove(budgetOverview);
            int result = await _dataContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToDeleteException<BudgetOverview>(budgetOverviewId);
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<List<Budget>> GetAllBudgets(int parentId)
        {
            List<Budget> budgets = await _dataContext.Budgets
                .Where(x => x.Parent == parentId)
                .ToListAsync();
            return budgets;
        }

        /// <inheritdoc/>
        public async Task<Budget> GetBudgetByCategory(int parentId, int categoryId)
        {
            Budget budget = await _dataContext.Budgets
                .Where(x => x.Parent == parentId && x.CategoryId == categoryId)
                .SingleAsync();
            return budget;
        }

        /// <inheritdoc/>
        public async Task<BudgetOverview> GetCurrentBudgetOverview(int userId, int id)
        {
            BudgetOverview budgetOverview = await _dataContext.BudgetOverviews
                .Where(x => x.OwnerId == userId && x.Id == id)
                .SingleAsync();
            return budgetOverview;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateBudget(int budgetId, float value)
        {
            Budget budgetToUpdate = await _dataContext.Budgets
                .Where(x => x.Id == budgetId)
                .SingleAsync();

            if (budgetToUpdate != null)
            {
                throw new NotFoundException(budgetId);
            }

            budgetToUpdate.Value = value;

            _dataContext.Budgets.Update(budgetToUpdate);
            int result = await _dataContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToUpdateException<Budget>(budgetId);
            }

            return true;
        }
    }
}
