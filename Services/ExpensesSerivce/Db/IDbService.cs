using Common.Models.Expenses;

namespace CurrencyService.Db
{
    public interface IDbService
    {
        Task<List<Expense>> GetAll(int ownerId);

        Task<Expense> Get(int ownerId, int Id);

        Task<List<Expense>> GetByCategory(int ownerId, int categoryId);

        Task<List<Expense>> GetByValue(int ownerId, int minValue, int maxValue);

        Task<bool> Create(CreateExpense expense);

        Task<bool> Delete(int ownerId, int Id);

        Task<bool> DeleteMonth(int ownerId, string month);

        Task<bool> DeleteAll(int ownerId);
    }
}
