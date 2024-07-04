using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Loans;
using Common.Models.Savings;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;

namespace UserService.Db
{
	public class DataContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataContext"/> class.
		/// </summary>
		/// <param name="options">The options for configuring the data context.</param>
		public DataContext(DbContextOptions<DataContext> options)
			: base(options) { }

		/// <summary>
		/// Gets or sets the collection of users.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Gets or sets the collection of user auths.
		/// </summary>
		public DbSet<UserAuth> UserAuths { get; set; }
	}
}
