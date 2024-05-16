using Common.Exceptions;
using Common.Models.PortfolioModels;
using Microsoft.EntityFrameworkCore;

namespace PortfolioService.Db
{
    public class DbService<T> : IDbService<T>
		where T : PortfolioModel
	{
		private readonly DataContext _context;

		public DbService(DataContext context)
		{
			_context = context;
		}

        /// <inheritdoc />
        public async Task<T> GetAsync(int id)
		{
			try
			{
                return await _context.Set<T>().Where(x => x.Id == id).SingleAsync();
			} 
			catch (Exception) 
			{
				throw new NotFoundException();
			};

		}

        /// <inheritdoc />
        public async Task<List<T>> GetAllAsync()
		{
			try
			{
				List<T> list = await _context.Set<T>().ToListAsync();
				if (list.Count == 0) throw new NotFoundException();

				return list;
			} catch (Exception)
			{
                throw new NotFoundException();
            }
		}
	}
}
