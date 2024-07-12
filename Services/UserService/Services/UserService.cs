using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.User;
using DbService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserService.Interfaces;

namespace UserService.Services
{
    [Authorize]
    public class UserService : IUserService
    {
        private readonly IDbService<User> _dbService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IValidation _validator;

        public UserService(IDbService<User> dbService, IPasswordHasher<User> passwordHasher, IValidation validator)
        {
            _dbService = dbService;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetUser(int id)
        {
            try
            {
                User? user = await _dbService.Get(id);
                if (user == null)
                    throw new NotFoundException();

                UserResponse userResponse = Creators.GetUserResponse(user);
                return userResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetUser(string username)
        {
            try
            {
                User? user = await _dbService.Get(x => x.Username == username);
                if (user == null)
                    throw new NotFoundException();

                UserResponse userResponse = Creators.GetUserResponse(user);
                return userResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<UserGroup> GetUsersFromUserGroup(Guid userGroup)
        {
            if (userGroup == Guid.Empty)
                throw new ArgumentNullException(nameof(userGroup));

            try
            {
                List<User>? users = await _dbService.GetAll(x => x.UserGroupId == userGroup);
                if (users == null || users.Count == 0)
                    throw new NotFoundException();

                UserGroup group = new UserGroup();
                foreach (User user in users)
                {
                    UserGroupSingleUser newSingleUser = new() { UserName = user.Username, Email = user.Email, };

                    group.Users.Add(newSingleUser);
                }

                return group;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateUser(UpdateUser updateUser)
        {
            try
            {
                User? user = await _dbService.Get(updateUser.Id);
                if (user == null)
                    throw new NotFoundException();

                if (!string.IsNullOrWhiteSpace(updateUser.Name))
                    user.Name = updateUser.Name;
                if (!string.IsNullOrWhiteSpace(updateUser.Surname))
                    user.Surname = updateUser.Surname;
                if (!string.IsNullOrWhiteSpace(updateUser.Email))
                    user.Email = updateUser.Email;
                if (!string.IsNullOrWhiteSpace(updateUser.Username))
                    user.Username = updateUser.Username;
                if (updateUser.CurrencyId != 0)
                    user.CurrencyId = updateUser.CurrencyId;

                User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                if (updatedUser == null)
                    throw new FailedToUpdateException<User>(user.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePassword(UpdatePassword updatePassword)
        {
            _validator.ValidateString(updatePassword.NewPassword);
            try
            {
                User? user = await _dbService.Get(updatePassword.Id);
                if (user == null)
                    throw new NotFoundException();

                _validator.ValidatePasswordRequest(
                    user.Password,
                    updatePassword.OldPassword,
                    _passwordHasher,
                    EAuthRequestType.UPDATE_PASSWORD,
                    user
                );

                string password = _passwordHasher.HashPassword(user, updatePassword.NewPassword);
                user.Password = password;

                User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                if (updatedUser == null)
                    throw new FailedToUpdateException<User>(user.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                User? deletedUser = await _dbService.Delete(x => x.Id == id);
                if (deletedUser == null)
                    throw new FailedToDeleteException<User>(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUser(CreateUser newUser)
        {
            _validator.ValidateCreateUser(newUser);
            string password = _passwordHasher.HashPassword(null, newUser.Password);

            User userToCreate =
                new()
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname,
                    Email = newUser.Email,
                    Username = newUser.Username,
                    Password = password,
                    CurrencyId = newUser.CurrencyId,
                    IsVerified = false,
                    CreatedAt = DateTime.Now,
                    IsBlocked = false,
                    LoginCounter = 0,
                    UserGroupId = Guid.Empty,
                };

            try
            {
                int user = await _dbService.Create(userToCreate);
                if (user == 0)
                    throw new FailedToCreateException<User>();
                // Todo: send email to allow account verification
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> VerifyUser(int id)
        {
            try
            {
                User? user = await _dbService.Get(id);
                if (user == null)
                    throw new NotFoundException();

                user.IsVerified = true;

                User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                if (updatedUser == null)
                    throw new FailedToUpdateException<User>(user.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UnblockUser(int id)
        {
            try
            {
                User? user = await _dbService.Get(id);
                if (user == null)
                    throw new NotFoundException();

                if (user.IsBlocked)
                {
                    user.IsBlocked = false;
                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                    if (updatedUser == null)
                        throw new FailedToUpdateException<User>(user.Id);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUserGroup(int[] userIds, Guid userGroupId)
        {
            if (userIds.Length < 2)
                return false;

            if (userGroupId == Guid.Empty)
                throw new ArgumentNullException(nameof(userGroupId));

            try
            {
                bool doesGroupAlreadyExist = await DoesUserGroupAlreadyExist(userGroupId);
                if (doesGroupAlreadyExist)
                    throw new RecordAlreadyExistException();

                List<User> users = new();

                foreach (int id in userIds)
                {
                    User? user = await _dbService.Get(id);
                    if (user == null)
                        throw new NotFoundException();
                    users.Add(user);
                }

                foreach (User user in users)
                {
                    bool userAdded = await AddUserToUserGroup(user.Id, userGroupId);
                    if (!userAdded)
                        throw new FailedToUpdateException<User>(user.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> AddUserToUserGroup(int userId, Guid userGroupId)
        {
            if (userGroupId == Guid.Empty)
                throw new ArgumentNullException(nameof(userGroupId));
            try
            {
                UserResponse? user = await GetUser(userId);
                if (user != null)
                {
                    user.UserGroupId = userGroupId;

                    User? updatedUser = await _dbService.Update(user, x => x.Id == userId);
                    if (updatedUser == null)
                        throw new FailedToUpdateException<User>();

                    return true;
                }

                throw new NotFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveUserFromUserGroup(int userId)
        {
            try
            {
                User? user = await _dbService.Get(userId);
                if (user != null)
                {
                    user.UserGroupId = Guid.Empty;

                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                    if (updatedUser == null)
                        throw new FailedToUpdateException<User>();

                    return true;
                }

                throw new NotFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUserGroup(Guid userGroup)
        {
            if (userGroup == Guid.Empty)
                throw new ArgumentNullException(nameof(userGroup));

            try
            {
                List<User>? users = await _dbService.GetAll(x => x.UserGroupId == userGroup);
                if (users == null || users.Count == 0)
                    throw new NotFoundException();

                foreach (User user in users)
                {
                    user.UserGroupId = Guid.Empty;
                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);

                    if (updatedUser == null)
                        throw new FailedToUpdateException<User>();
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesUserGroupAlreadyExist(Guid userGroupId)
        {
            if (userGroupId == Guid.Empty)
                throw new ArgumentNullException(nameof(userGroupId));

            try
            {
                User? user = await _dbService.Get(x => x.UserGroupId == userGroupId);
                if (user != null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
