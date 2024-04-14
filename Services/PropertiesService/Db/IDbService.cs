using Common.Models.Properties;

namespace LoansService.Db
{
    public interface IDbService
    {
        Task<List<Property>> GetAll(int ownerId);

    }
}