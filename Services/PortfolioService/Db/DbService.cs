using Common.Exceptions;
using Common.Models.PortfolioModels;
using Microsoft.EntityFrameworkCore;

namespace PortfolioService.Db
{
    public class DbService<T> : IDbService<T>
		where T : PortfolioModel
	{
		private readonly DataContext _context;

		public DbService(DataContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<bool> CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			int result = await _context.SaveChangesAsync();

			if (result == 0) throw new FailedToCreateException<T>();

			return true;
		}

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
		{
			try
			{
                T entity = await _context.Set<T>().Where(x => x.Id == id).SingleAsync();

                _context.Set<T>().Remove(entity);
                int result = await _context.SaveChangesAsync();
                if (result == 0) throw new FailedToDeleteException<T>(id);

                return true;
            }
            catch (Exception)
			{
                throw new NotFoundException();
			}			
		}

        /// <inheritdoc />
        public async Task<T> GetAsync(int id)
		{
			try
			{
                return await _context.Set<T>().Where(x => x.Id == id).SingleAsync();
			} 
			catch (Exception) 
			{
				throw new NotFoundException();
			};

		}

        /// <inheritdoc />
        public async Task<List<T>> GetAllAsync(int ownerId)
		{
			List<T> entities = await _context.Set<T>().Where(x => x.OwnerId == ownerId).ToListAsync();
			return entities;
		}

        /// <inheritdoc />
        public async Task<List<T>> GetAllAsync(int ownerId, int month, int year)
		{
			List<T> entities = await _context
				.Set<T>()
				.Where(x => x.OwnerId == ownerId && x.CreatedAt.Month == month && x.CreatedAt.Year == year)
				.ToListAsync();
			return entities;
		}

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(T entity)
		{
			try
			{
                T entityToUpdate = await _context.Set<T>().Where(x => x.Id == entity.Id).SingleAsync();

                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                int result = await _context.SaveChangesAsync();

                if (result == 0) throw new FailedToUpdateException<T>(entity.Id);

                return true;
            } catch (Exception)
			{                 
				throw new NotFoundException(entity.Id);
			}	
		}
	}
}
