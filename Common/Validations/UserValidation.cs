using Common.Enums;
using Common.Interfaces;
using Common.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Validations
{
    public class UserValidation : IUserValidation
    {
        /// <inheritdoc/>
        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password is required", nameof(password));

            Regex passwordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            Match match = passwordRegex.Match(password);
            if (!match.Success)
                throw new ArgumentException($"{nameof(password)} does not meet requirements");
        }

        /// <inheritdoc/>
        public void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("Username is required", nameof(username));

            Regex usernameRegex = new(@"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$");
            Match match = usernameRegex.Match(username);
            if (!match.Success)
                throw new ArgumentException($"{nameof(username)} does not meet requirements");
        }

        /// <inheritdoc/>
        public void ValidateNameAndSurname(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Username is required", nameof(name));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException("Username is required", nameof(surname));

            Regex regex = new(@"^[a-z ,.'-]+$/i");
            Match nameMatch = regex.Match(name);
            Match surnameMatch = regex.Match(surname);

            if (!nameMatch.Success)
                throw new ArgumentException($"{nameof(name)} does not meet requirements");

            if (!surnameMatch.Success)
                throw new ArgumentException($"{nameof(surname)} does not meet requirements");
        }

        /// <inheritdoc/>
        public void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Username is required", nameof(email));

            try
            {
                MailAddress address = new MailAddress(email);
                bool isValid = (address.Address == email);

                if (!isValid)
                    throw new ArgumentException($"{nameof(email)} does not meet requirements");
            }
            catch (FormatException)
            {
                throw new ArgumentException($"{nameof(email)} does not meet requirements");
            }
        }

        /// <inheritdoc/>
        public void ValidatePasswordRequest(
            string currentPassword,
            string oldPassword,
            IPasswordHasher<User> hasher,
            EAuthRequestType requestType,
            User user
        )
        {
            if (string.IsNullOrWhiteSpace(oldPassword))
                throw new ArgumentException("Old password is required", nameof(oldPassword));

            PasswordVerificationResult isOldPasswordValid = hasher.VerifyHashedPassword(user, currentPassword, oldPassword);

            if (isOldPasswordValid == PasswordVerificationResult.Failed)
                throw new ArgumentException("Old password is incorrect", nameof(oldPassword));
        }

        /// <inheritdoc/>
        public bool ValidatePasswordRequest(string currentPassword, string oldPassword, IPasswordHasher<User> hasher, User user)
        {
            PasswordVerificationResult isOldPasswordValid = hasher.VerifyHashedPassword(user, currentPassword, oldPassword);

            if (isOldPasswordValid == PasswordVerificationResult.Failed)
                return false;

            return true;
        }

        /// <inheritdoc/>
        public void ValidateString(string attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException(attribute, nameof(attribute));
        }
    }
}
