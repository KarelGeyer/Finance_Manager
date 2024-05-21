using Common.Exceptions;
using Common.Models;
using Common.Models.PortfolioModels;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Interfaces.Db;

namespace PortfolioService.Db
{
	public class DbService<T> : IDbService<T>
		where T : BaseDbModel
	{
		protected readonly DataContext _context;

		public DbService(DataContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<int> CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<int> DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			return await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<T?> GetAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		/// <inheritdoc />
		public async Task<int> UpdateAsync(T updatedEntity, T entityToUpdate)
		{
			_context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
			return await _context.SaveChangesAsync();
		}
	}
}
