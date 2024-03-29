using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models;
using Common.Models.Category;
using Common.Models.Savings;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SavingsService.Db;
using Supabase;
using Supabase.Gotrue;

namespace SavingsService.Service
{
    /// <summary>
    /// Represents a database service for handling savings.
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
        public async Task<bool> Create(int userId)
        {
            Savings newSavings = new Savings
            {
                OwnerId = userId,
                Amount = 0,
                Id = userId
            };
            await _context.Savings.AddAsync(newSavings);
            int created = await _context.SaveChangesAsync();
            if (created == 0)
            {
                throw new FailedToCreateException<Savings>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(int id)
        {
            Savings savings = await _context.Savings.Where(x => x.Id == id).SingleAsync();
            if (savings == null)
            {
                throw new NotFoundException();
            }

            _context.Savings.Remove(savings);
            int deleted = await _context.SaveChangesAsync();
            if (deleted == 0)
            {
                throw new FailedToDeleteException<Savings>(id);
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<double> Get(int userId)
        {
            Savings savings = await _context.Savings.Where(x => x.OwnerId == userId).SingleAsync();

            if (savings == null)
            {
                throw new NotFoundException();
            }

            return savings.Amount;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(UpdateSavings request)
        {
            Savings savings = await _context.Savings
                .Where(x => x.OwnerId == request.UserId)
                .SingleAsync();
            if (savings == null)
            {
                throw new FailedToUpdateException<Savings>();
            }

            savings.Amount = request.Amount;
            int updated = await _context.SaveChangesAsync();
            if (updated == 0)
            {
                throw new FailedToUpdateException<Savings>(savings.Id);
            }

            return true;
        }
    }
}
