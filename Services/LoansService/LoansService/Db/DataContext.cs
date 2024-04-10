using Common.Models.Loan;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoansService.Db
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
        public DbSet<Loan> Loans { get; set; }
    }
}