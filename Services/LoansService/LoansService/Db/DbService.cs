using Common.Enums;
using Common.Exceptions;
using Common.Models;
using Common.Models.Category;
using Common.Models.Loan;
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

        public async Task<bool> Create(CreateLoan createLoan)
        {
            Loan newLoan = new()
            {
                OwnerId = createLoan.OwnerId,
                Value = createLoan.Value,
                Name = createLoan.Name,
                To = createLoan.OwnToId,
                CreatedAt = DateTime.Now,
                Id = new Random().Next(1, 1000)
            };

            _context.Loans.Add(newLoan);
            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new FailedToCreateException<Loan>(newLoan.Id);
            }

            return true;
        }

        public async Task<bool> DeleteAllByMonth(int ownerId, string month)
        {
            List<Loan> loans = _context.Loans
                .Where(x =>
                    x.OwnerId == ownerId &&
                    x.CreatedAt.ToString("MMMM") == month)
                .ToList();

            _context.Loans.RemoveRange(loans);
            int resut = await _context.SaveChangesAsync();

            if (resut == 0)
            {
                throw new FailedToDeleteException<Loan>();
            }

            return true;
        }

        public async Task<bool> DeleteAllTo(int ownerId, int ownToId, string month)
        {
            List<Loan> loans = _context.Loans
                .Where(x =>
                    x.OwnerId == ownerId &&
                    x.To == ownToId &&
                    x.CreatedAt.ToString("MMMM") == month)
                .ToList();

            _context.Loans.RemoveRange(loans);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToDeleteException<Loan>();
            }

            return true;
        }

        public async Task<bool> DeleteAllToByMonth(int ownerId, int ownToId, string month)
        {
            List<Loan> loans = _context.Loans
                .Where(x =>
                x.OwnerId == ownerId &&
                x.To == ownToId &&
                x.CreatedAt.ToString("MMMM") == month)
                .ToList();

            _context.Loans.RemoveRange(loans);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToDeleteException<Loan>();
            }

            return true;
        }

        public async Task<bool> DeleteOne(int ownerId, int loanId)
        {
            Loan loan = _context.Loans
                .Where(x => x.OwnerId == ownerId && x.Id == loanId)
                .Single();

            _context.Loans.Remove(loan);
            int result = _context.SaveChanges();

            if (result == 0)
            {
                throw new FailedToDeleteException<Loan>();
            }

            return true;
        }

        public async Task<List<Loan>> GetAll(int ownerId)
        {
            List<Loan> loans = await _context.Loans
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync();

            return loans;
        }

        public async Task<List<Loan>> GetByDate(int ownerId, DateTime date)
        {
            List<Loan> loans = await _context.Loans
                .Where(x => x.OwnerId == ownerId && x.CreatedAt == date)
                .ToListAsync();

            return loans;
        }

        public async Task<Loan> GetById(int ownerId, int loanId)
        {
            Loan loan = await _context.Loans
                .Where(x => x.OwnerId == ownerId && x.Id == loanId)
                .SingleAsync();

            if (loan == null)
            {
                throw new NotFoundException(loanId);
            }

            return loan;
        }

        public async Task<List<Loan>> GetByMonth(int ownerId, string month)
        {
            List<Loan> loans = await _context.Loans
                .Where(x => x.OwnerId == ownerId && x.CreatedAt.ToString("MMMM") == month)
                .ToListAsync();

            return loans;
        }

        public async Task<List<Loan>> GetByOwnToId(int ownerId, int ownToId)
        {
            List<Loan> loans = await _context.Loans
                .Where(x => x.OwnerId == ownerId && x.To == ownToId)
                .ToListAsync();

            return loans;
        }

        public async Task<bool> Update(UpdateLoan request)
        {
            Loan loan = await _context.Loans.Where(x => x.Id == request.Id).SingleAsync();
            if (loan == null)
            {
                throw new NotFoundException(request.Id);
            }

            loan.Name = request.Name;
            loan.Value = request.Value;

            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new FailedToUpdateException<Loan>(request.Id);
            }

            return true;
        }
    }
}