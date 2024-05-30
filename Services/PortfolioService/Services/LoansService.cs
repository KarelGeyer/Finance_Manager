using Common.Models.ProductModels.Loans;
using DbService;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
	public class LoansService : ILoansService
	{
		IDbService<Loan> _dbService;

		public LoansService(IDbService<Loan> dbService)
		{
			_dbService = dbService;
		}

		/// <inheritdoc />
		public async Task<List<Loan>> GetLoansByOwnTo(int ownerId, int ownTo)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));
			if (ownTo == 0)
				throw new ArgumentException(nameof(ownTo));

			try
			{
				return await _dbService.GetAll(x => x.OwnerId == ownerId && x.ToPerson == ownTo);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<double> GetTotalDebt(int ownerId)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

			try
			{
				return await _dbService.GetSum(x => x.Value, x => x.OwnerId == ownerId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<double> GetTotalDebt(int ownerId, int ownTo)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));
			if (ownTo == 0)
				throw new ArgumentException(nameof(ownTo));

			try
			{
				return await _dbService.GetSum(x => x.Value, x => x.OwnerId == ownerId && x.ToPerson == ownTo);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
