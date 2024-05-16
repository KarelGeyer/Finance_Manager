using Microsoft.EntityFrameworkCore;
using StaticDataService.Db;

namespace StaticDataServiceTests.Db
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
