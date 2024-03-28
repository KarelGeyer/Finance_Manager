using Common.Models.Currency;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using Postgrest;

namespace UserService.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ClientOptions>();

            modelBuilder
                .Entity<UserModel>()
                .Ignore(b => b.BaseUrl)
                .HasOne<UserGroupModel>(u => u.UserGroup)
                .WithMany(ug => ug.Users)
                .HasForeignKey(u => u.UserGroupId);

            modelBuilder
                .Entity<UserModel>()
                .Ignore(b => b.BaseUrl)
                .HasOne<CurrencyModel>(u => u.Currency)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CurrencyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
