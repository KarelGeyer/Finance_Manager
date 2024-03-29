using Common.Enums;
using Common.Models;
using Common.Models.Category;
using Common.Models.Savings;

namespace SavingsService.Service
{
    public interface IDbService
    {
        /// <summary>
        /// Gets the amount of savings for the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The amount of savings.</returns>
        /// <exception cref="Common.Exceptions.NotFoundException"></exception>
        Task<double> Get(int userId);

        /// <summary>
        /// Creates a new savings record for the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        /// <exception cref="Common.Exceptions.FailedToCreateException{Savings}"></exception>
        Task<bool> Create(int userId);

        /// <summary>
        /// Updates the amount of savings for the specified user.
        /// </summary>
        /// <param name="request">The update request.</param>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        /// <exception cref="Common.Exceptions.FailedToUpdateException{Savings}"></exception>
        Task<bool> Update(UpdateSavings request);

        /// <summary>
        /// Deletes the savings record with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the savings record to delete.</param>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        /// <exception cref="Common.Exceptions.NotFoundException"></exception>
        /// <exception cref="Common.Exceptions.FailedToDeleteException{Savings}"></exception>
        Task<bool> Delete(int userId);
    }
}
