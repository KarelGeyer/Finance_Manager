using Common.Models.Savings;
using Microsoft.EntityFrameworkCore;

namespace SavingsService.Db
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Savings> Savings { get; set; }
    }
}
