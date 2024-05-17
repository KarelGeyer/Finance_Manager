using Common.Models.PortfolioModels;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Db;
using PortfolioService.Interfaces;

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
	}
}
