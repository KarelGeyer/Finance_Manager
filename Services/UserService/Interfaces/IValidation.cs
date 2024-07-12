using Common.Enums;
using Common.Models.User;
using Microsoft.AspNetCore.Identity;

namespace UserService.Interfaces
{
    public interface IValidation
    {
        /// <summary>
        /// Validate the data from <see cref="CreateUser"/> user request
        /// </summary>
        /// <param name="user">create user data</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void ValidateCreateUser(CreateUser user);

        /// <summary>
        /// Validate the data when requesting any password manipulation
        /// </summary>
        /// <param name="currentPassword">current user password stored in DB</param>
        /// <param name="oldPassword">password added to the form for comparison</param>
        /// <param name="hasher"><see cref="IPasswordHasher"/> to hash password</param>
        /// <param name="requestType">type of password manipulation</param>
        /// <exception cref="ArgumentException"/>
        public void ValidatePasswordRequest(
            string currentPassword,
            string oldPassword,
            IPasswordHasher<User> hasher,
            EAuthRequestType requestType,
            User user
        );

        /// <summary>
        /// Validate the data when requesting any password manipulation
        /// </summary>
        /// <param name="currentPassword">current user password stored in DB</param>
        /// <param name="oldPassword">password added to the form for comparison</param>
        /// <param name="hasher"><see cref="IPasswordHasher"/> to hash password</param>
        public bool ValidatePasswordRequest(string currentPassword, string oldPassword, IPasswordHasher<User> hasher, User user);

        /// <summary>
        /// Validate any string
        /// </summary>
        /// <param name="attribute">string to validate</param>
        /// <exception cref="ArgumentException"/>
        public void ValidateString(string attribute);
    }
}
