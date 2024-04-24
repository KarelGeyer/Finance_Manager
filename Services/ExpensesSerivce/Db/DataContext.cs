using Common.Models.Currency;
using Common.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Db
{
    /// <summary>
    /// Represents a data context for managing income data.
    /// </summary>
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
        public DbSet<Expense> Expenses { get; set; }
    }
}
