using Common.Models.ProductModels.Loans;
using DbService;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
    public class LoansService : ILoansService
    {
        IDbService<Loan> _dbService;
        private readonly ILogger<LoansService> _logger;

        public LoansService(IDbService<Loan> dbService, ILogger<LoansService> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<Loan>> GetLoansByOwnTo(int ownerId, int ownTo)
        {
            _logger.LogInformation($"{nameof(GetLoansByOwnTo)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }
            if (ownTo == 0)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${nameof(ownTo)} must be provided");
                throw new ArgumentException(nameof(ownTo));
            }

            try
            {
                return await _dbService.GetAll(x => x.OwnerId == ownerId && x.ToPerson == ownTo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<double> GetTotalDebt(int ownerId)
        {
            _logger.LogInformation($"{nameof(GetTotalDebt)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }

            try
            {
                return await _dbService.GetSum(x => x.Value, x => x.OwnerId == ownerId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetTotalDebt)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<double> GetTotalDebt(int ownerId, int ownTo)
        {
            _logger.LogInformation($"{nameof(GetTotalDebt)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }
            if (ownTo == 0)
            {
                _logger.LogError($"{nameof(GetLoansByOwnTo)} - ${nameof(ownTo)} must be provided");
                throw new ArgumentException(nameof(ownTo));
            }

            try
            {
                return await _dbService.GetSum(x => x.Value, x => x.OwnerId == ownerId && x.ToPerson == ownTo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetTotalDebt)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
