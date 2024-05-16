using Common.Models.Category;
using Common.Models.Currency;
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
        /// Gets or sets the collection of currency records.
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets the categories DbSet.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the category types DbSet.
        /// </summary>
        public DbSet<CategoryType> CategoriesTypes { get; set; }
    }
}
