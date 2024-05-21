using Common.Models.ProductModels.Loans;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Interfaces.Db;

namespace PortfolioService.Db
{
	public class LoansDbService : ILoansDbService
	{
		private readonly DataContext _context;

		public LoansDbService(DataContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<List<Loan>> GetAllByOwnTo(int ownerId, int ownTo)
		{
			return await _context.Set<Loan>().Where(x => x.OwnerId == ownerId && x.ToPerson == ownTo).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<double> GetTotalDebth(int ownerId)
		{
			return await _context.Set<Loan>().Where(x => x.OwnerId == ownerId).SumAsync(x => x.Value);
		}

		/// <inheritdoc />
		public async Task<double> GetTotalDebthByOwnTo(int ownerId, int ownTo)
		{
			return await _context.Set<Loan>().Where(x => x.OwnerId == ownerId && x.ToPerson == ownTo).SumAsync(x => x.Value);
		}
	}
}
