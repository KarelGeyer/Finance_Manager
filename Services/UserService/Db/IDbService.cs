using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.User;

namespace UserService.Service
{
    /// <summary>
    /// Represents a database service for managing user records.
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Retrieves all user records.
        /// </summary>
        /// <returns>A list of user records.</returns>
        Task<List<User>> GetAll();

        /// <summary>
        /// Retrieves a user record by ID.
        /// </summary>
        /// <param name="id">The ID of the user record.</param>
        /// <returns> The user record. </returns>
        /// <exception cref="NotFoundException">Thrown when the user record is not found.</exception>"
        Task<User> GetById(int id);

        /// <summary>
        /// Creates a new user record.
        /// </summary>
        /// <param name="req">The user create request.</param>
        /// <returns> True if the user record is created successfully, otherwise false. </returns>
        /// <exception cref="FailedToCreateException{User}"> Thrown when the user record fails to be created. </exception>
        Task<bool> Create(CreateUser req);

        /// <summary>
        /// Updates user record.
        /// </summary>
        /// <param name="req">The user update request.</param>
        /// <returns> True if the user record is updated successfully, otherwise false. </returns>
        /// <exception cref="FailedToUpdateException{User}"> Thrown when the user record fails to be updated. </exception>
        Task<bool> Update(UpdateUser req);

        /// <summary>
        /// Updates the password of a user record.
        /// </summary>
        /// <param name="req">The user update password request.</param>
        /// <returns> True if the user record is updated successfully, otherwise false. </returns>
        /// <exception cref="FailedToUpdateException{User}"> Thrown when the user record fails to be updated. </exception>"
        Task<bool> UpdatePassword(UpdateUserPassword req);

        /// <summary>
        /// Deletes a user record.
        /// </summary>
        /// <param name="id">The ID of the user record to delete.</param>
        /// <returns> True if the user record is deleted successfully, otherwise false. </returns>
        /// <exception cref="NotFoundException"> Thrown when the user record is not found. </exception>"
        /// <exception cref="FailedToDeleteException{User}"> Thrown when the user record fails to be deleted. </exception>"
        Task<bool> Delete(int id);
    }
}
