using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models;
using Common.Models.Category;
using Common.Models.Savings;
using Common.Models.User;
using Microsoft.VisualBasic;
using Supabase;

namespace SavingsService.Service
{
    public class DbService : IDbService
    {
        private readonly Client _supabaseClient;

        public DbService(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<bool> Create(int userId)
        {
            Savings newSavings = new Savings { OwnerId = userId, Amount = 0, };
            var response = await _supabaseClient.From<Savings>().Insert(newSavings);

            if (response == null)
            {
                throw new FailedToCreateException<Savings>();
            }

            return true;
        }

        public async Task<bool> Delete(int userId)
        {
            await _supabaseClient.From<Savings>().Where(s => s.OwnerId == userId).Delete();
            return true;
        }

        public async Task<float> Get(int userId)
        {
            var response = await _supabaseClient
                .From<Savings>()
                .Where(s => s.OwnerId == userId)
                .Single();

            if (response == null)
            {
                throw new NotFoundException();
            }

            return response.Amount;
        }

        public async Task<bool> Update(UpdateSavings request)
        {
            var response = await _supabaseClient
                .From<Savings>()
                .Where(x => x.OwnerId == request.Id)
                .Set(x => x.Amount, request.Amount)
                .Update();

            if (response.Models == null || response.Models.Count == 0)
            {
                throw new FailedToUpdateException<Savings>();
            }

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
