using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Category;
using Supabase;

namespace CategoryService.Service
{
    public class DbService<T> : IDbService<T>
        where T : BaseDbModel, new()
    {
        private readonly Client _supabaseClient;

        public DbService(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        /// <inheritdoc/>
        public async Task<List<T>> GetAllAsync()
        {
            var response = await _supabaseClient.From<T>().Get();

            if (response.Models == null || response.Models.Count == 0)
            {
                throw new NotFoundException();
            }

            return response.Models;
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync(int id)
        {
            var response = await _supabaseClient.From<T>().Where(x => x.Id == id).Single();

            if (response == null)
            {
                throw new NotFoundException(id);
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetByCategoryAsync(int categoryTypeId)
        {
            var response = await _supabaseClient
                .From<Category>()
                .Where(x => x.TypeId == categoryTypeId)
                .Get();

            if (response.Models == null || response.Models.Count == 0)
            {
                throw new NotFoundException();
            }

            return response.Models;
        }
    }
}
