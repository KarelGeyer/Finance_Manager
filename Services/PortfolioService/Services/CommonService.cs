using Common.Exceptions;
using Common.Models.PortfolioModels;
using DbService;
using Common.Interfaces;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
    public class CommonService<T> : ICommonService<T>
        where T : PortfolioModel
    {
        protected readonly IDbService<T> _dbService;
        private readonly IPortfolioValidation<T> _validation;
        private readonly ILogger<CommonService<T>> _logger;

        public CommonService(IPortfolioValidation<T> validation, IDbService<T> dbService, ILogger<CommonService<T>> logger)
        {
            _dbService = dbService;
            _validation = validation;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<bool> CreateEntity(T entity)
        {
            _logger.LogInformation($"{nameof(CreateEntity)} - method start");
            _validation.ValidatePortfolioModel(entity);

            try
            {
                int newEntity = await _dbService.Create(entity);
                if (newEntity == 0)
                {
                    _logger.LogError($"{nameof(CreateEntity)} - failed to create new entity");
                    throw new FailedToCreateException<T>(entity.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateEntity)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<bool> CreateEntity(T entity, int id)
        {
            _logger.LogInformation($"{nameof(CreateEntity)} - method start");
            _validation.ValidatePortfolioModel(entity);

            try
            {
                int newEntity = await _dbService.Create(entity);
                if (newEntity == 0)
                {
                    _logger.LogError($"{nameof(CreateEntity)} - failed to create new entity");
                    throw new FailedToCreateException<T>(entity.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateEntity)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<bool> UpdateEntity(T entity)
        {
            _logger.LogInformation($"{nameof(CreateEntity)} - method start");
            _validation.ValidatePortfolioModel(entity);

            try
            {
                T? result = await _dbService.Update(entity, x => x.Id == entity.Id);
                if (result is null)
                {
                    _logger.LogError($"{nameof(CreateEntity)} - failed to update entity");
                    throw new FailedToUpdateException<T>(entity.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateEntity)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<bool> DeleteEntity(int id)
        {
            _logger.LogInformation($"{nameof(DeleteEntity)} - method start");
            if (id == 0)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${nameof(id)} must be provided");
                throw new ArgumentException(nameof(id));
            }

            try
            {
                T? deletedEntity = await _dbService.Delete(x => x.Id == id);
                if (deletedEntity == null)
                {
                    _logger.LogError($"{nameof(CreateEntity)} - no entity was found");
                    throw new NotFoundException(id);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<T> GetEntity(int id)
        {
            _logger.LogInformation($"{nameof(GetEntity)} - method start");
            if (id == 0)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${nameof(id)} must be provided");
                throw new ArgumentException(nameof(id));
            }

            T? entity = await _dbService.Get(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogError($"{nameof(CreateEntity)} - no entity was found");
                throw new NotFoundException(id);
            }

            return entity;
        }

        /// <inheritdoc />
        public async Task<List<T>> GetEntitiesSortedByDate(int ownerId)
        {
            _logger.LogInformation($"{nameof(GetEntitiesSortedByDate)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }

            try
            {
                List<T> entities = await _dbService.GetAll(x => x.OwnerId == ownerId);
                return entities.OrderBy(x => x.CreatedAt).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetEntitiesSortedByDate)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> GetEntities(int ownerId)
        {
            _logger.LogInformation($"{nameof(GetEntities)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }

            try
            {
                return await _dbService.GetAll(x => x.OwnerId == ownerId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetEntities)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> GetEntities(int ownerId, int month, int year)
        {
            _logger.LogInformation($"{nameof(GetEntities)} - method start");
            if (ownerId == 0)
            {
                _logger.LogError($"{nameof(DeleteEntity)} - ${nameof(ownerId)} must be provided");
                throw new ArgumentException(nameof(ownerId));
            }

            int monthToFilter = month == 0 || month > 12 ? DateTime.Now.Month : month;
            int yearToFilter = year == 0 || year > DateTime.Now.Year ? DateTime.Now.Year : year;

            try
            {
                return await _dbService.GetAll(x => x.OwnerId == ownerId && x.CreatedAt.Month == month && x.CreatedAt.Year == year);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetEntities)} - ${ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
