using Common.Exceptions;
using Common.Models.PortfolioModels;
using DbService;
using PortfolioService.Interfaces;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
	public class CommonService<T> : ICommonService<T>
		where T : PortfolioModel
	{
		protected readonly IDbService<T> _dbService;
		private readonly IValidation<T> _validation;

		public CommonService(IValidation<T> validation, IDbService<T> dbService)
		{
			_dbService = dbService;
			_validation = validation;
		}

		/// <inheritdoc />
		public async Task<bool> CreateEntity(T entity)
		{
			_validation.ValidatePortfolioModel(entity);

			try
			{
				int newEntity = await _dbService.Create(entity);
				if (newEntity == 0)
					throw new FailedToCreateException<T>(entity.Id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<bool> UpdateEntity(T entity)
		{
			_validation.ValidatePortfolioModel(entity);

			try
			{
				T? result = await _dbService.Update(entity, x => x.Id == entity.Id);
				if (result is null)
					throw new FailedToUpdateException<T>(entity.Id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<bool> DeleteEntity(int id)
		{
			if (id == 0)
				throw new ArgumentException(nameof(id));
			try
			{
				T? deletedEntity = await _dbService.Delete(x => x.Id == id);
				if (deletedEntity == null)
					throw new NotFoundException(id);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<T> GetEntity(int id)
		{
			if (id == 0)
				throw new ArgumentException(nameof(id));
			T? entity = await _dbService.Get(x => x.Id == id);
			if (entity == null)
				throw new NotFoundException(id);
			return entity;
		}

		/// <inheritdoc />
		public async Task<List<T>> GetEntitiesSortedByDate(int ownerId)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

			try
			{
				List<T> entities = await _dbService.GetAll(x => x.OwnerId == ownerId);
				return entities.OrderBy(x => x.CreatedAt).ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<T>> GetEntities(int ownerId)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

			try
			{
				return await _dbService.GetAll(x => x.OwnerId == ownerId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<T>> GetEntities(int ownerId, int month, int year)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

			int monthToFilter = month == 0 || month > 12 ? DateTime.Now.Month : month;
			int yearToFilter = year == 0 || year > DateTime.Now.Year ? DateTime.Now.Year : year;

			try
			{
				return await _dbService.GetAll(x => x.OwnerId == ownerId && x.CreatedAt.Month == month && x.CreatedAt.Year == year);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
