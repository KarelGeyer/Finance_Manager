using Common.Enums;
using Common.Models;
using Common.Models.User;

namespace UserService.Service
{
    public interface IDbService<T>
        where T : BaseDbModel, new()
    {
        /// <summary>
        /// Retrieves all users from the db
        /// </summary>
        /// <returns>Task with list of users</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Attempts to find a user using provided id
        /// </summary>
        /// <returns>Task with a user</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Retrieves all users of a given type from the db
        /// </summary>
        /// <returns>Task with list of users</returns>
        Task<List<UserModel>> GetByUserAsync(int Id);
    }
}
