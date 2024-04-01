using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models;
using Common.Models.Category;
using Common.Models.Income;
using Common.Models.Savings;
using IncomeService.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SavingsService.Db;
using Supabase;
using Supabase.Gotrue;

namespace IncomeService.Db
{
    /// <summary>
    /// Database service for managing income data.
    /// </summary>
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbService"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public DbService(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<bool> Create(IncomeCreateRequest req)
        {
            Income newIncome = new Income
            {
                Id = req.OwnerId + 4,
                OwnerId = req.OwnerId,
                Name = req.Name,
                Value = req.Value,
                CategoryId = req.CategoryId,
                CreatedAt = DateTime.Now,
            };

            await _context.Incomes.AddAsync(newIncome);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToCreateException<Income>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(int id)
        {
            Income income = await _context.Incomes.Where(x => x.Id == id).SingleAsync();
            if (income == null)
            {
                throw new NotFoundException();
            }

            _context.Incomes.Remove(income);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToDeleteException<Income>(id);
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<Income> Get(int userId, int id)
        {
            Income income = await _context.Incomes
                .Where(x => x.OwnerId == userId && x.Id == id)
                .SingleAsync();
            if (income == null)
            {
                throw new NotFoundException();
            }

            return income;
        }

        /// <inheritdoc/>
        public async Task<List<Income>> GetAll(int userId)
        {
            List<Income> incomes = await _context.Incomes
                .Where(x => x.OwnerId == userId)
                .ToListAsync();
            if (incomes == null)
            {
                return new List<Income>();
            }

            return incomes;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(IncomeUpdateNameRequest req)
        {
            Income income = await _context.Incomes.Where(x => x.Id == req.Id).SingleAsync();
            if (income == null)
            {
                throw new NotFoundException();
            }

            income.Name = req.Name;
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Income>(req.Id);
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(IncomeUpdateValueRequest req)
        {
            Income income = await _context.Incomes.Where(x => x.Id == req.Id).SingleAsync();
            if (income == null)
            {
                throw new NotFoundException();
            }

            income.Value = req.Value;
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Income>(req.Id);
            }

            return true;
        }
    }
}
