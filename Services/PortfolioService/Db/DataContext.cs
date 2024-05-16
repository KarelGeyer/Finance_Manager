using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Loans;
using Common.Models.ProductModels.Properties;
using Common.Models.Savings;
using Microsoft.EntityFrameworkCore;

namespace PortfolioService.Db
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
		/// Gets or sets the collection of income records.
		/// </summary>
		public DbSet<Property> Properties { get; set; }

		/// <summary>
		/// Gets or sets the collection of currency records.
		/// </summary>
		public DbSet<Expense> Expenses { get; set; }

		/// <summary>
		/// Gets or sets the collection of income records.
		/// </summary>
		public DbSet<Income> Incomes { get; set; }

		/// <summary>
		/// Gets or sets the collection of income records.
		/// </summary>
		public DbSet<Loan> Loans { get; set; }

		/// <summary>
		/// Gets or sets the savings entities.
		/// </summary>
		public DbSet<Savings> Savings { get; set; }

		/// <summary>
		/// Gets or sets the budgets DbSet.
		/// </summary>
		public DbSet<Budget> Budgets { get; set; }
	}
}
