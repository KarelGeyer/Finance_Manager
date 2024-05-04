using Common.Exceptions;
using Common.Models.Currency;
using Common.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Db
{
    /// <summary>
    /// Database service for managing income data.
    /// </summary>
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CreateExpense expense)
        {
            Expense newExpense = new Expense
            {
                OwnerId = expense.OwnerId,
                Name = expense.Name,
                Value = expense.Value,
                CategoryId = expense.CategoryId
            };

            await _context.Expenses.AddAsync(newExpense);
            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<Expense>(newExpense.Id);
            }

            return true;
        }

        public async Task<bool> Delete(int ownerId, int Id)
        {
            Expense? expense = await _context.Expenses.FindAsync(Id);
        }

        public Task<bool> DeleteAll(int ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMonth(int ownerId, string month)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> Get(int ownerId, int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Expense>> GetAll(int ownerId)
        {
            DateTime date = DateTime.Now;

            List<Expense> expenses = await _context.Expenses
                .Where(x => x.OwnerId == ownerId && x.CreatedAt.Month == date.Month)
                .ToListAsync();

            return expenses;
        }

        public Task<List<Expense>> GetByCategory(int ownerId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Expense>> GetByValue(int ownerId, int minValue, int maxValue)
        {
            throw new NotImplementedException();
        }
    }
}
