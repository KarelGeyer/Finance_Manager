using Common.Exceptions;
using Common.Models.Category;
using DbService;
using StaticDataService.Interfaces;

namespace StaticDataService.Services
{
    public class CommonService<T> : ICommonService<T>
    {
        private readonly IDbService<T> _dbService;

        public CommonService(IDbService<T> dbService)
        {
            _dbService = dbService;
        }

        /// <inheritdoc />
        public async Task<List<T>> GetEntities()
        {
            try
            {
                List<T> list = await _dbService.GetAll();
                if (list.Count.Equals(0))
                    throw new NotFoundException();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<T> GetEntity(int id)
        {
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
                throw new Exception(ex.Message);
            }
        }
    }
}
