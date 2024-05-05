using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Category;
using Common.Models.Loan;
using Common.Models.ProductModels.Properties;
using Microsoft.EntityFrameworkCore;
using Supabase;

namespace LoansService.Db
{
    public class DbService : IDbService
    {
        private readonly DataContext _context;

        public DbService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CreateProperty createProperty)
        {
            Property newProperty = new Property
            {
                OwnerId = createProperty.OwnerId,
                Name = createProperty.Name,
                Value = createProperty.Value,
                CategoryId = createProperty.CategoryId,
                CreatedAt = DateTime.Now
            };

            await _context.Properties.AddAsync(newProperty);
            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<Property>(newProperty.Id);
            }

            return true;
        }

        public async Task<bool> DeleteAll(int ownerId)
        {
            List<Property> properties = await _context.Properties
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync();

            _context.Properties.RemoveRange(properties);
            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToDeleteException<Property>();
            }

            return true;
        }

        public async Task<bool> DeleteById(int ownerId, int id)
        {
            Property property = await _context.Properties
                .Where(x => x.OwnerId == ownerId && x.Id == id)
                .SingleAsync();

            _context.Properties.Remove(property);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToDeleteException<Property>(id);
            }

            return true;
        }

        public async Task<Property> Get(int ownerId)
        {
            Property property = await _context.Properties
                .Where(x => x.OwnerId == ownerId)
                .SingleAsync();

            if (property == null)
            {
                throw new NotFoundException();
            }

            return property;
        }

        public async Task<List<Property>> GetAll(int ownerId)
        {
            List<Property> properties = await _context.Properties
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync();

            if (properties == null)
            {
                return new List<Property>();
            }

            return properties;
        }

        public async Task<List<Property>> GetByCategory(int ownerId, int categoryId)
        {
            List<Property> properties = await _context.Properties
                .Where(x => x.OwnerId == ownerId && x.CategoryId == categoryId)
                .ToListAsync();

            if (properties == null)
            {
                return new List<Property>();
            }

            return properties;
        }

        public async Task<bool> UpdateName(int ownerId, string name)
        {
            Property property = await _context.Properties
                .Where(x => x.OwnerId == ownerId)
                .SingleAsync();

            property.Name = name;
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Property>(property.Id);
            }

            return true;
        }

        public async Task<bool> UpdateValue(int ownerId, double value)
        {
            Property property = await _context.Properties
                .Where(x => x.OwnerId == ownerId)
                .SingleAsync();

            property.Value = value;
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Property>(property.Id);
            }

            return true;
        }
    }
}
