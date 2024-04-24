using Common.Enums;
using Common.Models;
using Common.Models.Budget;
using Common.Models.Category;

namespace BudgetService.Db
{
    public interface IDbService
    {
        Task<List<Budget>> GetAllBudgets(int parentId);

        Task<Budget> GetBudgetByCategory(int parentId, int categoryId);

        Task<bool> CreateBudget(Budget budget);

        Task<bool> UpdateBudget(int budgetId, float value);

        Task<bool> DeleteBudget(int id, int parent);

        Task<BudgetOverview> GetCurrentBudgetOverview(int userId, int id);

        Task<bool> CreateBudgetOverview(BudgetOverviewRequest budgetOverview);

        Task<bool> DeleteBudgetOverview(int userId, int budgetOverviewId);
    }
}
