using Common.Enums;
using Common.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Common.Interfaces
{
    public interface IUserValidation
    {
        /// <summary>
        /// Rules for password validation:
        /// <list type="bullet">
        /// <item>Minimum of 8 characters</item>
        /// <item>Maximum od 32 characters</item>
        /// <item>At least 1 uppercase character</item>
        /// <item>At least 1 lowercase character</item>
        /// <item>At least 1 number</item>
        /// <item>At least 1 special character</item>
        /// </list>
        /// </summary>
        /// <param name="password">password</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        void ValidatePassword(string password);

        /// <summary>
        /// Rules for password validation:
        /// <list type="bullet">
        /// <item>Minimum of 8 characters</item>
        /// <item>Maximum od 20 characters</item>
        /// <item>Cannot have . or _ at the beginning or at the end</item>
        /// <item>Cannot have .. or __ or ._ or _. at the beginning</item>
        /// <item>Only allows letters, numbers and symbols . _</item>
        /// </list>
        /// </summary>
        /// <param name="password">username</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        void ValidateUsername(string username);

        /// <summary>
        /// Rules for name and surname allows most of the usecases.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="surname">surname</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        void ValidateNameAndSurname(string name, string surname);

        /// <summary>
        /// Allows classic <a href="https://pdw.ex-parrot.com/Mail-RFC822-Address.html">RFC-822</a> for email validation.
        /// </summary>
        /// <param name="email">email</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        void ValidateEmail(string email);

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
