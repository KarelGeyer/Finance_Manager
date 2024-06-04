using Common.Models.ProductModels.Loans;

namespace PortfolioService.Interfaces.Services
{
	public interface ILoansService
	{
		/// <summary>
		/// Calls a db service to retrieve all entities for a specific owner and a person they are indebted to.
		/// </summary>
		/// <param name="ownerId">User Id</param>
		/// <param name="ownTo">Id of the debtor</param>
		/// <returns>List of <see cref="Loan"/></returns>
		/// <exception cref="Exception"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<List<Loan>> GetLoansByOwnTo(int ownerId, int ownTo);

		/// <summary>
		/// Calls a db service to retrieve an amount a user owns
		/// </summary>
		/// <param name="ownerId">User id</param>
		/// <returns>A total sum of debts</returns>
		/// <exception cref="Exception"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<double> GetTotalDebt(int ownerId);

		/// <summary>
		/// Calls a db service to retrieve an amount a user owns to a debtor.
		/// </summary>
		/// <param name="ownerId">User Id</param>
		/// <param name="ownTo">Id of the debtor</param>
		/// <returns>A total sum of debts to a debtor></returns>
		/// <exception cref="Exception"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<double> GetTotalDebt(int ownerId, int ownTo);
	}
}
