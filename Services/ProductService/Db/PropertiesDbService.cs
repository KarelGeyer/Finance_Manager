using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Category;
using Common.Models.Loan;
using Common.Models.ProductModels.Properties;
using Microsoft.EntityFrameworkCore;
using Supabase;

namespace ProductService.Db
{
    public class DbService<T> : IDbService<T>
        where T : BaseDbModel
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<T>();
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await _context.Set<T>().Where(x => x.Id == id).SingleAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _context.Set<T>().Remove(entity);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToDeleteException<T>(id);
            }

            return true;
        }

        public async Task<T> GetAsync(int id)
        {
            T entity = await _context.Set<T>().Where(x => x.Id == id).SingleAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<List<T>> GetAllAsync(int ownerId, int month)
        {
            List<T> entities = await _context.FindAsync<T>
                .Where(x => x.OwnerId == ownerId && x.CreatedAt.Month == month)
                .ToListAsync();

            if (properties == null)
            {
                return new List<T>();
            }

            return entities;
        }

        public async Task<bool> UpdateAsync(Property entity)
        {
            Property property = await _context.Properties
                .Where(x => x.Id == entity.Id)
                .SingleAsync();

            property = entity;
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Property>(property.Id);
            }

            return true;
        }

        Task<List<T>> IDbService<T>.GetAllAsync(int ownerId, int month)
        {
            throw new NotImplementedException();
        }

        Task<T> IDbService<T>.GetAsync(int ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
