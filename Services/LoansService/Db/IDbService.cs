using Common.Enums;
using Common.Models;
using Common.Models.Category;
using Common.Models.Loan;

namespace LoansService.Db
{
    public interface IDbService
    {
        Task<List<Loan>> GetAll(int ownerId);

        Task<Loan> GetById(int ownerId, int loanId);
        
        Task<List<Loan>> GetByOwnToId(int ownerId, int ownToId);
        
        Task<List<Loan>> GetByMonth(int ownerId, string month);
        
        Task<List<Loan>> GetByDate(int ownerId, DateTime date);

        Task<bool> Create(CreateLoan createLoan);

        Task<bool> Update(UpdateLoan req);

        Task<bool> DeleteOne(int ownerId, int loanId);

        Task<bool> DeleteAllToByMonth(int ownerId, int ownToId, string month);

        Task<bool> DeleteAllTo(int ownerId, int ownToId, string month);

        Task<bool> DeleteAllByMonth(int ownerId, string month);
    }
}
