using Microsoft.EntityFrameworkCore;
using PortfolioService.Db;

namespace PortfolioServiceTests.Db
{
	public static class GetContext
	{
		public static DataContext GetDbContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Data_Context");
			var context = new DataContext(optionsBuilder.Options);
			return context;
		}
	}
}
