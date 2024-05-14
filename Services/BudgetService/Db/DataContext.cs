using Common.Models.Budget;
using Microsoft.EntityFrameworkCore;
using BudgetService.Controllers;

namespace BudgetService.Db
{
    /// <summary>
    /// Represents the data context for the category service.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the budgets DbSet.
        /// </summary>
        public virtual DbSet<Budget> Budgets { get; set; }

        /// <summary>
        /// Gets or sets the budget overview DbSet.
        /// </summary>
        public virtual DbSet<BudgetOverview> BudgetOverviews { get; set; }
    }
}
