using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Income;

namespace IncomeService.Db
{
    /// <summary>
    /// Represents a database service for managing income records.
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Retrieves all income records for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of income records.</returns>
        Task<List<Income>> GetAll(int userId);

        /// <summary>
        /// Retrieves an income record by ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="id">The ID of the income record.</param>
        /// <returns>The income record.</returns>
        /// <exception cref="NotFoundException">Thrown when the income record is not found.</exception>
        Task<Income> Get(int userId, int id);

        /// <summary>
        /// Creates a new income record.
        /// </summary>
        /// <param name="req">The income create request.</param>
        /// <returns>True if the income record is created successfully, otherwise false.</returns>
        /// <exception cref="FailedToCreateException{Income}">Thrown when the income record fails to be created.</exception>
        Task<bool> Create(IncomeCreateRequest req);

        /// <summary>
        /// Updates the name of an income record.
        /// </summary>
        /// <param name="req">The income update name request.</param>
        /// <returns>True if the income record is updated successfully, otherwise false.</returns>
        /// <exception cref="NotFoundException">Thrown when the income record is not found.</exception>
        /// <exception cref="FailedToUpdateException{Income}">Thrown when the income record fails to be updated.</exception>
        Task<bool> Update(IncomeUpdateNameRequest req);

        /// <summary>
        /// Updates the value of an income record.
        /// </summary>
        /// <param name="req">The income update value request.</param>
        /// <returns>True if the income record is updated successfully, otherwise false.</returns>
        /// <exception cref="NotFoundException">Thrown when the income record is not found.</exception>
        /// <exception cref="FailedToUpdateException{Income}">Thrown when the income record fails to be updated.</exception>
        Task<bool> Update(IncomeUpdateValueRequest req);

        /// <summary>
        /// Deletes an income record.
        /// </summary>
        /// <param name="id">The ID of the income record to delete.</param>
        /// <returns>True if the income record is deleted successfully, otherwise false.</returns>
        /// <exception cref="NotFoundException">Thrown when the income record is not found.</exception>
        /// <exception cref="FailedToDeleteException{Income}">Thrown when the income record fails to be deleted.</exception>
        Task<bool> Delete(int id);
    }
}
