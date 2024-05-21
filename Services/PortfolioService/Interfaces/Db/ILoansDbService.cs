using Common.Models.ProductModels.Loans;

namespace PortfolioService.Interfaces.Db
{
	public interface ILoansDbService
	{
		/// <summary>
		/// Retrieves all entities for a specific owner and a person they are indebted to.
		/// </summary>
		/// <param name="ownerId">User Id</param>
		/// <param name="ownTo">Id of the debtor</param>
		/// <returns>List of <see cref="Loan"/></returns>
		/// <exception cref="Exception"></exception>
		Task<List<Loan>> GetAllByOwnTo(int ownerId, int ownTo);

		/// <summary>
		/// Retrieves an amount a user owns
		/// </summary>
		/// <param name="ownerId">User id</param>
		/// <returns>A total sum of debts</returns>
		/// <exception cref="Exception"></exception>
		Task<double> GetTotalDebth(int ownerId);

		/// <summary>
		/// Retrieves an amount a user owns to a debtor.
		/// </summary>
		/// <param name="ownerId">User Id</param>
		/// <param name="ownTo">Id of the debtor</param>
		/// <returns>A total sum of debts to a debtor></returns>
		/// <exception cref="Exception"></exception>
		Task<double> GetTotalDebthByOwnTo(int ownerId, int ownTo);
	}
}
