using Common.Models.User;
using Microsoft.EntityFrameworkCore;

namespace UserService.Db
{
    /// <summary>
    /// Represents a data context for managing user data.
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
        /// Gets or sets the collection of user records.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
