using Common.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Db
{
    /// <summary>
    /// Represents the data context for the category service.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the categories DbSet.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the category types DbSet.
        /// </summary>
        public virtual DbSet<CategoryType> CategoriesTypes { get; set; }
    }
}
