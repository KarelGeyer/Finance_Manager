using Common.Exceptions;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using StaticDataService.Db;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
	public class DbService<T> : IDbService<T>
		where T : BaseDbModel
	{
		private readonly DataContext _context;

		public DbService(DataContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<T?> GetAsync(int id)
		{
			T? entity = await _context.Set<T>().FindAsync(id);
			return entity;
		}

		/// <inheritdoc />
		public async Task<List<T>> GetAllAsync()
		{
			List<T> list = await _context.Set<T>().ToListAsync();
			return list;
		}
	}
}
