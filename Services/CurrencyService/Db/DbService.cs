using Common.Exceptions;
using Common.Models.Currency;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Db
{
    /// <summary>
    /// Database service for managing income data.
    /// </summary>
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Currency> Get(int id)
        {
            Currency currency = await _context.Currencies.Where(x => x.Id == id).SingleAsync();
            if (currency == null)
            {
                throw new NotFoundException(id);
            }

            return currency;
        }

        /// <inheritdoc/>
        public async Task<List<Currency>> GetAll()
        {
            List<Currency> currencies = await _context.Currencies.ToListAsync();
            if (currencies == null || currencies.Count == 0)
            {
                throw new NotFoundException();
            }

            return currencies;
        }
    }
}
