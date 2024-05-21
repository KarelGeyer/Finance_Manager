using Common.Interfaces;
using Common.Models.PortfolioModels;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Db;
using PortfolioService.Interfaces.Db;

namespace PortfolioService.Services
{
	public class CommonDbService<T> : DbService<T>, ICommonDbService<T>
		where T : PortfolioModel
	{
		public CommonDbService(DataContext context)
			: base(context) { }

		/// <inheritdoc />
		public async Task<List<T>> GetAllByDateAsync(int ownerId, int month, int year)
		{
			return await _context.Set<T>().Where(x => x.OwnerId == ownerId && x.CreatedAt.Month == month && x.CreatedAt.Year == year).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<List<T>> GetAllByOwnerAsync(int ownerId)
		{
			return await _context.Set<T>().Where(x => x.OwnerId == ownerId).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<List<P>> GetAllByOwnerSortedByDateAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel
		{
			List<P> entities = await _context.Set<P>().Where(x => x.OwnerId == ownerId).OrderBy(x => x.CreatedAt).ToListAsync();
			if (isReversedOrder)
				entities.Reverse();
			return entities;
		}

		/// <inheritdoc />
		public async Task<List<P>> GetAllByOwnerSortedByNameAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel
		{
			List<P> entities = await _context.Set<P>().Where(x => x.OwnerId == ownerId).OrderBy(x => x.Name).ToListAsync();
			if (isReversedOrder)
				entities.Reverse();
			return entities;
		}

		/// <inheritdoc />
		public async Task<List<P>> GetAllByOwnerSortedByValueAsync<P>(int ownerId, bool isReversedOrder)
			where P : CommonPortfolioModel
		{
			List<P> entities = await _context.Set<P>().Where(x => x.OwnerId == ownerId).OrderBy(x => x.Value).ToListAsync();
			if (isReversedOrder)
				entities.Reverse();
			return entities;
		}

		/// <inheritdoc />
		public async Task<List<P>> GetAllByCategory<P>(int ownerId, int categoryId)
			where P : CommonPortfolioModel
		{
			List<P> entities = await _context.Set<P>().Where(x => x.OwnerId == ownerId && x.CategoryId == categoryId).ToListAsync();
			return entities;
		}
	}
}
