using Common.Models.Savings;
using Microsoft.EntityFrameworkCore;

namespace SavingsService.Db
{
    /// <summary>
    /// Represents the data context for the savings service.
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
        /// Gets or sets the savings entities.
        /// </summary>
        public DbSet<Savings> Savings { get; set; }
    }
}
