using Common.Models.User;
using Common.Exceptions;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Attempts to retrieve a user from database by id
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns><see cref="User"/> user</returns>
        /// <exception cref="NotFoundException" />
        /// <exception cref="Exception" />
        Task<UserResponse> GetUser(int id);

        /// <summary>
        /// Attempts to retrieve a user from database by id
        /// </summary>
        /// <param name="username">username</param>
        /// <returns><see cref="User"/> user</returns>
        /// <exception cref="NotFoundException" />
        /// <exception cref="Exception" />
        Task<UserResponse> GetUser(string username);

        /// <summary>
        /// Attempts to retrieve a list of users based on user group
        /// </summary>
        /// <param name="userGroup">userGroup</param>
        /// <returns><see cref="User"/> user</returns>
        /// <exception cref="NotFoundException" />
        /// <exception cref="Exception" />
        Task<UserGroup> GetUsersFromUserGroup(Guid userGroup);

        /// <summary>
        /// Attempts to update User's data
        /// </summary>
        /// <param name="updateUser">user to update</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> UpdateUser(UpdateUser updateUser);

        /// <summary>
        /// Attempts to delete user's account
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="FailedToDeleteException" />
        Task<bool> DeleteUser(int id);

        /// <summary>
        /// Attempts to create a new user account
        /// </summary>
        /// <param name="newUser">new user</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="FailedToCreateException" />
        Task<bool> CreateUser(CreateUser newUser);

        /// <summary>
        /// Attempts to create group for accounts specified by given argument
        /// </summary>
        /// <param name="userIds">array of user id's</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="RecordAlreadyExistException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> CreateUserGroup(int[] userIds, Guid userGroup);

        /// <summary>
        /// Attempts to add a user to a user group
        /// </summary>
        /// <param name="userIds">array of user id's</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> AddUserToUserGroup(int userId, Guid userGroup);

        /// <summary>
        /// Attempts to remove a user from a user group
        /// </summary>
        /// <param name="userIds">array of user id's</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> RemoveUserFromUserGroup(int userId);

        /// <summary>
        /// Attempts to delete a userGroup
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException{T}{T}" />
        Task<bool> DeleteUserGroup(Guid userGroup);

        /// <summary>
        /// Attempts to change user's password
        /// </summary>
        /// <param name="updatePassword">update password</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> UpdatePassword(UpdatePassword updatePassword);

        /// <summary>
        /// Verify whetever user account has already been verified
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> VerifyUser(int id);

        /// <summary>
        /// Attempts to unblock user account
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        /// <exception cref="NotFoundException" />
        /// <exception cref="FailedToUpdateException" />
        Task<bool> UnblockUser(int id);

        /// <summary>
        /// Check whether a user with provided GUID usergroup id already exists
        /// </summary>
        /// <param name="id">User group id</param>
        /// <returns>a bool value</returns>
        /// <exception cref="Exception" />
        Task<bool> DoesUserGroupAlreadyExist(Guid userGroupId);
    }
}
