using Common.Models.Income;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IncomeService.Db
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
        /// Gets or sets the collection of income records.
        /// </summary>
        public DbSet<Income> Incomes { get; set; }
    }
}
