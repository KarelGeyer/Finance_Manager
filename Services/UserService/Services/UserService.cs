using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Interfaces;
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
        private readonly IUserValidation _validator;
        private readonly ILogger<UserService> _logger;

        public UserService(IDbService<User> dbService, IPasswordHasher<User> passwordHasher, IUserValidation validator, ILogger<UserService> logger)
        {
            _dbService = dbService;
            _passwordHasher = passwordHasher;
            _validator = validator;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetUser(int id)
        {
            _logger.LogInformation($"{nameof(GetUser)} - method start");
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
                _logger.LogError($"{nameof(GetUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetUser(string username)
        {
            _logger.LogInformation($"{nameof(GetUser)} - method start");
            try
            {
                User? user = await _dbService.Get(x => x.Username == username);
                if (user == null)
                {
                    _logger.LogError($"{nameof(GetUser)} - user was not found");
                    throw new NotFoundException();
                }

                UserResponse userResponse = Creators.GetUserResponse(user);
                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<UserGroup> GetUsersFromUserGroup(Guid userGroup)
        {
            _logger.LogInformation($"{nameof(GetUsersFromUserGroup)} - method start");
            if (userGroup == Guid.Empty)
            {
                _logger.LogError($"{nameof(GetUsersFromUserGroup)} - {nameof(userGroup)} cannot be empty");
                throw new ArgumentNullException(nameof(userGroup));
            }

            try
            {
                List<User>? users = await _dbService.GetAll(x => x.UserGroupId == userGroup);
                if (users == null || users.Count == 0)
                {
                    _logger.LogError($"{nameof(GetUsersFromUserGroup)} - users were not found");
                    throw new NotFoundException();
                }

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
                _logger.LogError($"{nameof(GetUsersFromUserGroup)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateUser(UpdateUser updateUser)
        {
            _logger.LogInformation($"{nameof(UpdateUser)} - method start");
            try
            {
                User? user = await _dbService.Get(updateUser.Id);
                if (user == null)
                {
                    _logger.LogError($"{nameof(UpdateUser)} - user could not be found");
                    throw new NotFoundException();
                }

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
                {
                    _logger.LogError($"{nameof(UpdateUser)} - user could not be updated");
                    throw new FailedToUpdateException<User>(user.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePassword(UpdatePassword updatePassword)
        {
            _logger.LogInformation($"{nameof(UpdatePassword)} - method start");
            _validator.ValidateString(updatePassword.NewPassword);
            try
            {
                User? user = await _dbService.Get(updatePassword.Id);
                if (user == null)
                {
                    _logger.LogError($"{nameof(UpdatePassword)} - user could not be found");
                    throw new NotFoundException();
                }

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
                {
                    _logger.LogError($"{nameof(UpdatePassword)} - user could not be updated");
                    throw new FailedToUpdateException<User>(user.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdatePassword)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUser(int id)
        {
            _logger.LogInformation($"{nameof(DeleteUser)} - method start");
            try
            {
                User? deletedUser = await _dbService.Delete(x => x.Id == id);
                if (deletedUser == null)
                    throw new FailedToDeleteException<User>(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUser(CreateUser newUser)
        {
            _logger.LogInformation($"{nameof(CreateUser)} - method start");

            _validator.ValidatePassword(newUser.Password);
            _validator.ValidateEmail(newUser.Email);
            _validator.ValidateNameAndSurname(newUser.Name, newUser.Surname);
            _validator.ValidateUsername(newUser.Username);

            try
            {
                User? user = await _dbService.Get(x => x.Username == newUser.Username);
                if (user != null)
                {
                    _logger.LogError($"{nameof(CreateUser)} - user with this username already exists");
                    throw new UserAlreadyExistsException();
                }

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

                int userCreated = await _dbService.Create(userToCreate);

                if (userCreated == 0)
                {
                    _logger.LogError($"{nameof(CreateUser)} - user could not be created");
                    throw new FailedToCreateException<User>();
                }

                // Todo: send email to allow account verification

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> VerifyUser(int id)
        {
            _logger.LogInformation($"{nameof(VerifyUser)} - method start");
            try
            {
                User? user = await _dbService.Get(id);
                if (user == null)
                {
                    _logger.LogError($"{nameof(VerifyUser)} - user could not be found");
                    throw new NotFoundException();
                }

                user.IsVerified = true;

                User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                if (updatedUser == null)
                {
                    _logger.LogError($"{nameof(VerifyUser)} - user could not be updated");
                    throw new FailedToUpdateException<User>(user.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VerifyUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UnblockUser(int id)
        {
            _logger.LogInformation($"{nameof(UnblockUser)} - method start");
            try
            {
                User? user = await _dbService.Get(id);
                if (user == null)
                {
                    _logger.LogError($"{nameof(UnblockUser)} - user could not be found");
                    throw new NotFoundException();
                }

                if (user.IsBlocked)
                {
                    user.IsBlocked = false;
                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                    if (updatedUser == null)
                    {
                        _logger.LogError($"{nameof(UnblockUser)} - user could not be updated");
                        throw new FailedToUpdateException<User>(user.Id);
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UnblockUser)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUserGroup(int[] userIds, Guid userGroupId)
        {
            _logger.LogInformation($"{nameof(CreateUserGroup)} - method start");
            if (userIds.Length < 2)
            {
                _logger.LogError($"{nameof(CreateUserGroup)} - user group requires at least 2 members");
                return false;
            }

            if (userGroupId == Guid.Empty)
            {
                _logger.LogError($"{nameof(CreateUserGroup)} - {nameof(userGroupId)} cannot be empty");
                throw new ArgumentNullException(nameof(userGroupId));
            }

            try
            {
                bool doesGroupAlreadyExist = await DoesUserGroupAlreadyExist(userGroupId);
                if (doesGroupAlreadyExist)
                {
                    _logger.LogError($"{nameof(CreateUserGroup)} - {nameof(userGroupId)} with this id already exists");
                    throw new RecordAlreadyExistException();
                }

                List<User> users = new();

                foreach (int id in userIds)
                {
                    User? user = await _dbService.Get(id);
                    if (user == null)
                    {
                        _logger.LogError($"{nameof(CreateUserGroup)} - user could not be found");
                        throw new NotFoundException();
                    }
                    users.Add(user);
                }

                foreach (User user in users)
                {
                    bool userAdded = await AddUserToUserGroup(user.Id, userGroupId);
                    if (!userAdded)
                    {
                        _logger.LogError($"{nameof(CreateUserGroup)} - user could not be updated");
                        throw new FailedToUpdateException<User>(user.Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateUserGroup)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> AddUserToUserGroup(int userId, Guid userGroupId)
        {
            _logger.LogInformation($"{nameof(AddUserToUserGroup)} - method start");
            if (userGroupId == Guid.Empty)
            {
                _logger.LogError($"{nameof(AddUserToUserGroup)} - {nameof(userGroupId)} cannot be empty");
                throw new ArgumentNullException(nameof(userGroupId));
            }

            try
            {
                User? user = await _dbService.Get(userId);
                if (user != null)
                {
                    user.UserGroupId = userGroupId;

                    User? updatedUser = await _dbService.Update(user, x => x.Id == userId);
                    if (updatedUser == null)
                    {
                        _logger.LogError($"{nameof(AddUserToUserGroup)} - user could not be updated");
                        throw new FailedToUpdateException<User>(user.Id);
                    }

                    return true;
                }

                _logger.LogError($"{nameof(AddUserToUserGroup)} - user could not be found");
                throw new NotFoundException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AddUserToUserGroup)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveUserFromUserGroup(int userId)
        {
            _logger.LogInformation($"{nameof(RemoveUserFromUserGroup)} - method start");
            try
            {
                User? user = await _dbService.Get(userId);
                if (user != null)
                {
                    user.UserGroupId = Guid.Empty;

                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);
                    if (updatedUser == null)
                    {
                        _logger.LogError($"{nameof(RemoveUserFromUserGroup)} - user could not be updated");
                        throw new FailedToUpdateException<User>(user.Id);
                    }

                    return true;
                }

                _logger.LogError($"{nameof(RemoveUserFromUserGroup)} - user could not be found");
                throw new NotFoundException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(RemoveUserFromUserGroup)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUserGroup(Guid userGroup)
        {
            _logger.LogInformation($"{nameof(DeleteUserGroup)} - method start");
            if (userGroup == Guid.Empty)
            {
                _logger.LogError($"{nameof(DeleteUserGroup)} - {nameof(userGroup)} cannot be empty");
                throw new ArgumentNullException(nameof(userGroup));
            }

            try
            {
                List<User>? users = await _dbService.GetAll(x => x.UserGroupId == userGroup);
                if (users == null || users.Count == 0)
                {
                    _logger.LogError($"{nameof(DeleteUserGroup)} - no users found under this user group id");
                    throw new NotFoundException();
                }

                foreach (User user in users)
                {
                    user.UserGroupId = Guid.Empty;
                    User? updatedUser = await _dbService.Update(user, x => x.Id == user.Id);

                    if (updatedUser == null)
                    {
                        _logger.LogError($"{nameof(DeleteUserGroup)} - user could not be updated");
                        throw new FailedToUpdateException<User>(user.Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteUserGroup)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesUserGroupAlreadyExist(Guid userGroupId)
        {
            _logger.LogInformation($"{nameof(DoesUserGroupAlreadyExist)} - method start");
            if (userGroupId == Guid.Empty)
            {
                _logger.LogError($"{nameof(DoesUserGroupAlreadyExist)} - {nameof(userGroupId)} cannot be empty");
                throw new ArgumentNullException(nameof(userGroupId));
            }

            try
            {
                User? user = await _dbService.Get(x => x.UserGroupId == userGroupId);
                if (user != null)
                {
                    _logger.LogWarning($"{nameof(DoesUserGroupAlreadyExist)} - usergroup with this {nameof(userGroupId)} already exists");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DoesUserGroupAlreadyExist)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
