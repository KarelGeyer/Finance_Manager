using Common.Models.Properties;

namespace LoansService.Db
{
    public interface IDbService
    {
        Task<List<Property>> GetAll(int ownerId);

        Task<Property> Get(int ownerId);

        Task<List<Property>> GetByCategory(int ownerId, int categoryId);

        Task<bool> Create(CreateProperty createProperty);

        Task<bool> UpdateName(int ownerId, string name);

        Task<bool> UpdateValue(int ownerId, double value);

        Task<bool> DeleteById(int ownerId, int id);

        Task<bool> DeleteAll(int ownerId);
    }
}