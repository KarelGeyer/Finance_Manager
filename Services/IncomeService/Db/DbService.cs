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

namespace SavingsService.Service
{
    public class DbService : IDbService
    {
        private readonly Client _supabaseClient;
        private readonly DataContext _context;

        public DbService(Client supabaseClient, DataContext context)
        {
            _supabaseClient = supabaseClient;
            _context = context;
        }

        public async Task<bool> Create(int userId)
        {
            //Savings newSavings = new Savings { OwnerId = userId, Amount = 0, };
            //var response = await _supabaseClient.From<Savings>().Insert(newSavings);

            //if (response == null)
            //{
            //    throw new FailedToCreateException<Savings>();
            //}

            return true;
        }

        public async Task<bool> Delete(int userId)
        {
            //await _supabaseClient.From<Savings>().Where(s => s.OwnerId == userId).Delete();
            return true;
        }

        public async Task<double> Get(int userId)
        {
            //var response = await _supabaseClient
            //    .From<Savings>()
            //    .Where(s => s.OwnerId == userId)
            //    .Single();

            var savings = await _context.Savings.ToListAsync();

            if (savings == null)
            {
                throw new NotFoundException();
            }

            return savings[0].Amount;
        }

        public async Task<bool> Update(UpdateSavings request)
        {
            //var response = await _supabaseClient
            //    .From<Savings>()
            //    .Where(x => x.OwnerId == request.Id)
            //    .Set(x => x.Amount, request.Amount)
            //    .Update();

            //if (response.Models == null || response.Models.Count == 0)
            //{
            //    throw new FailedToUpdateException<Savings>();
            //}

            return true;
        }

        public async Task GetUserId(int userId)
        {
            var user = await _supabaseClient.From<User>().Where(x => x.Id == userId).Single();
            if (user == null || user.Id == 0)
            {
                throw new UserDoesNotExistException();
            }
        }
    }
}
