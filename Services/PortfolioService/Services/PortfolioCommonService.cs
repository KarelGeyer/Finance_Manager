using Common.Enums;
using Common.Models.PortfolioModels;
using DbService;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
    public class PortfolioCommonService<T> : IPortfolioCommonService<T>
        where T : CommonPortfolioModel
    {
        protected IDbService<T> _dbService;
        private readonly ILogger<LoansService> _logger;

        public PortfolioCommonService(IDbService<T> dbService, ILogger<LoansService> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<T>> GetAllPortfolioEntitiesByGroup(int[] userIds)
        {
            _logger.LogInformation($"{nameof(GetAllPortfolioEntitiesByGroup)} - method start");
            if (userIds.Length < 2)
                throw new ArgumentException(nameof(userIds));

            try
            {
                List<T> portfolioEntities = new List<T>();
                foreach (var userId in userIds)
                {
                    List<T> entities = await _dbService.GetAll(x => x.OwnerId == userId);
                    portfolioEntities.AddRange(entities);
                }

                return portfolioEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetAllPortfolioEntitiesByGroup)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public List<T> GetCommonPortfolioEntitiesSortedByGivenParameter(int ownerId, bool shouldBeInReversedOrder, EPortfolioModelSortBy parameter)
        {
            _logger.LogInformation($"{nameof(GetCommonPortfolioEntitiesSortedByGivenParameter)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(GetCommonPortfolioEntitiesSortedByGivenParameter)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }

            try
            {
                List<T> entities;

                switch (parameter)
                {
                    case EPortfolioModelSortBy.Name:
                        entities = _dbService.GetAll(x => x.Name, x => x.OwnerId == ownerId);
                        break;
                    case EPortfolioModelSortBy.Value:
                        entities = _dbService.GetAll(x => x.Value, x => x.OwnerId == ownerId);
                        break;
                    case EPortfolioModelSortBy.Date:
                        entities = _dbService.GetAll(x => x.CreatedAt, x => x.OwnerId == ownerId);
                        break;
                    default:
                        _logger.LogError(
                            $"{nameof(GetCommonPortfolioEntitiesSortedByGivenParameter)} - ${nameof(parameter)} incorrect value was provided"
                        );
                        throw new ArgumentException(nameof(parameter));
                }

                if (shouldBeInReversedOrder)
                    entities.Reverse();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetCommonPortfolioEntitiesSortedByGivenParameter)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> GetCommonPortfolioEntitiesByCategory(int ownerId, int categoryId)
        {
            _logger.LogInformation($"{nameof(GetCommonPortfolioEntitiesByCategory)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(GetCommonPortfolioEntitiesByCategory)} - ${ownerId} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }
            if (categoryId == 0)
            {
                _logger.LogError($"{nameof(GetCommonPortfolioEntitiesByCategory)} - ${categoryId} must be provided");
                throw new ArgumentException(nameof(categoryId));
            }

            try
            {
                return await _dbService.GetAll(x => x.OwnerId == ownerId && x.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetCommonPortfolioEntitiesByCategory)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
