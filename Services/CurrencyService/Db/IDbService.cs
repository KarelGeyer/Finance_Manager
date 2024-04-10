using Common.Exceptions;
using Common.Models.Currency;

namespace CurrencyService.Db
{
    public interface IDbService
    {
        /// <summary>
        /// Retrieves all currencies from the db
        /// </summary>
        /// <returns>Task with list of categories</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<List<Currency>> GetAll();

        /// <summary>
        /// Attempts to find a currency using provided id
        /// </summary>
        /// <returns>Task with a category</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<Currency> Get(int id);
    }
}
