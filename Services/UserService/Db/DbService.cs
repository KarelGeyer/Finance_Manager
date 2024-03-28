using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using UserService.Db;

namespace UserService.Service
{
    public class DbService<T> : IDbService<T>
        where T : BaseDbModel, new()
    {
        private readonly AppDbContext _context;

        public DbService(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<List<T>> GetAllAsync()
        {
            var models = await _context.Set<T>().ToListAsync();

            if (models == null || models.Count == 0)
            {
                throw new NotFoundException();
            }

            return models;
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);

            if (model == null)
            {
                throw new NotFoundException(id);
            }

            return model;
        }

        public Task<List<UserModel>> GetByUserAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
