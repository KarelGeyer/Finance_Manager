using System.Linq.Expressions;

namespace DbService
{
    public interface IDbService<T>
    {
        /// <summary>
        /// retrieves a list of specific T entity.
        /// </summary>
        /// <param name="where">query</param>
        /// <returns>Entity</returns>
        Task<List<T>> GetAll(Expression<Func<T, bool>>? where = null);

        /// <summary>
        /// retrieves a list of specific T entity ordered by TKey/>.
        /// </summary>
        /// <param name="keySelector">an order by selector</param>
        /// <param name="where">query</param>
        /// <returns>Entity</returns>
        List<T> GetAll<TKey>(Func<T, TKey> keySelector, Expression<Func<T, bool>>? where = null);

        /// <summary>
        /// Gets a total sum of a specific value in a list of T entities.
        /// </summary>
        /// <param name="selector">a selector of a <see cref="decimal"/> value of an entity</param>
        /// <returns>a total sum of </returns>
        Task<decimal> GetSum(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? where = null);

        /// <summary>
        /// Gets a total sum of a specific value in a list of T entities.
        /// </summary>
        /// <param name="selector">a selector of a <see cref="double"/> value of an entity</param>
        /// <returns>a total sum of </returns>
        Task<double> GetSum(Expression<Func<T, double>> selector, Expression<Func<T, bool>>? where = null);

        /// <summary>
        /// retrieves a specific T entity.
        /// </summary>
        /// <param name="predicateToGetId">A query</param>
        /// <returns>Entity</returns>
        Task<T?> Get(Expression<Func<T, bool>> predicateToGetId);

        /// <summary>
        /// retrieves a specific T entity.
        /// </summary>
        /// <param name="id">A primary key to find by</param>
        /// <returns>Entity</returns>
        Task<T?> Get(int id);

        /// <summary>
        /// Creates a new T entity.
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns>true if created, else false</returns>
        Task<int> Create(T entity);

        /// <summary>
        /// Updates a T entity.
        /// </summary>
        /// <param name="entity">Entity with new values</param>
        /// <param name="where">A query</param>
        /// <returns>true if updated, else false</returns>
        Task<T?> Update(T entity, Expression<Func<T, bool>>? where = null);

        /// <summary>
        /// Deletes a T entity.
        /// </summary>
        /// <param name="where">A query</param>
        /// <returns>true if deleted, else false</returns>
        Task<T?> Delete(Expression<Func<T, bool>>? where);
    }
}
