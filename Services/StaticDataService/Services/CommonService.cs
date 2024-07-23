using Common.Exceptions;
using Common.Models.Category;
using DbService;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
    public class CommonService<T> : ICommonService<T>
    {
        private readonly IDbService<T> _dbService;
        private readonly ILogger<CommonService<T>> _logger;

        public CommonService(IDbService<T> dbService, ILogger<CommonService<T>> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<T>> GetEntities()
        {
            _logger.LogInformation($"{nameof(GetEntities)} - method start");
            try
            {
                List<T> list = await _dbService.GetAll();
                if (list.Count.Equals(0))
                    throw new NotFoundException();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetEntities)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<T> GetEntity(int id)
        {
            _logger.LogInformation($"{nameof(GetEntity)} - method start");
            if (id == 0 || id < 0)
                throw new ArgumentNullException("id");

            try
            {
                T? entity = await _dbService.Get(id);
                if (entity == null)
                    throw new NotFoundException(id);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetEntity)} - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
