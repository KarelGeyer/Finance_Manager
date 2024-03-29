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
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

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

        public async Task<double> Get(int userId)
        {
            Savings savings = await _context.Savings.Where(x => x.OwnerId == userId).SingleAsync();

            if (savings == null)
            {
                throw new NotFoundException();
            }

            return savings.Amount;
        }

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
