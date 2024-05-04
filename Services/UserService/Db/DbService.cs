using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using UserService.Db;

namespace UserService.Service
{
    /// <summary>
    /// Database service for managing user data.
    /// </summary>
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<bool> Create(CreateUser req)
        {
            User user = new User
            {
                UserGroupId = 1,
                Name = req.Name,
                Surname = req.Surname,
                Username = req.Username,
                Email = req.Email,
                Password = req.Password,
                CurrencyId = req.CurrencyId,
                IsVerified = false,
                CreatedAt = DateTime.Now,
            };

            await _context.Users.AddAsync(user);

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<User>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(int id)
        {
            User user = await _context.Users.Where(x => x.Id == id).SingleAsync();

            if (user == null)
            {
                throw new NotFoundException();
            }

            _context.Users.Remove(user);

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToDeleteException<User>(id);
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return new List<User>();
            }

            return users;
        }

        /// <inheritdoc/>
        public async Task<User> GetById(int id)
        {
            User user = await _context.Users.Where(x => x.Id == id).SingleAsync();

            if (user == null)
            {
                throw new NotFoundException();
            }

            return user;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(UpdateUser req)
        {
            User user = await _context.Users.Where(x => x.Id == req.Id).SingleAsync();

            if (user == null)
            {
                throw new NotFoundException();
            }

            user.Username = req.Username;
            user.Email = req.Email;
            user.CurrencyId = req.CurrencyId;

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToUpdateException<User>();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePassword(UpdateUserPassword req)
        {
            User user = await _context.Users.Where(x => x.Id == req.Id).SingleAsync();

            if (user == null)
            {
                throw new NotFoundException();
            }

            user.Password = req.Password;

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToUpdateException<User>();
            }

            return true;
        }
    }
}
