using Common.Models.ProductModels.Loans;
using PortfolioService.Db;
using PortfolioService.Interfaces;
using PortfolioService.Interfaces.Db;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
	public class LoansService : PortfolioCommonService<Loan>, ILoansService
	{
		ILoansDbService _loansDbService;

		public LoansService(ILoansDbService loansDbService, ICommonDbService<Loan> commonDbService, IValidation<Loan> validation)
			: base(commonDbService, validation)
		{
			_loansDbService = loansDbService;
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
				return await _loansDbService.GetAllByOwnTo(ownerId, ownTo);
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
				return await _loansDbService.GetTotalDebth(ownerId);
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
				return await _loansDbService.GetTotalDebthByOwnTo(ownerId, ownTo);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
