using Common.Enums;
using Common.Exceptions;
using Common.Models.PortfolioModels;
using PortfolioService.Interfaces;
using PortfolioService.Interfaces.Db;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Services
{
	public class PortfolioCommonService<T> : IPortfolioCommonService<T>
		where T : PortfolioModel
	{
		private readonly ICommonDbService<T> _commonDbService;
		private readonly IValidation<T> _validation;

		public PortfolioCommonService(ICommonDbService<T> commonDbService, IValidation<T> validation)
		{
			_commonDbService = commonDbService;
			_validation = validation;
		}

		/// <inheritdoc />
		public async Task<bool> CreateEntity(T entity)
		{
			_validation.ValidatePortfolioModel(entity);

			try
			{
				int result = await _commonDbService.CreateAsync(entity);
				if (result == 0)
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
				T? entityToUpdate = await _commonDbService.GetAsync(entity.Id);
				if (entityToUpdate == null)
					throw new NotFoundException(entity.Id);

				int result = await _commonDbService.UpdateAsync(entity, entityToUpdate);
				if (result == 0)
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
				T? entityToDelete = await _commonDbService.GetAsync(id);
				if (entityToDelete == null)
					throw new NotFoundException(id);

				int result = await _commonDbService.DeleteAsync(entityToDelete);
				if (result == 0)
					throw new FailedToDeleteException<T>(id);
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
			T? entity = await _commonDbService.GetAsync(id);
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
				List<T> entities = await _commonDbService.GetAllByOwnerAsync(ownerId);
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
				return await _commonDbService.GetAllByOwnerAsync(ownerId);
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
				return await _commonDbService.GetAllByDateAsync(ownerId, monthToFilter, yearToFilter);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<P>> GetCommonPortfolioEntitiesSortedByGivenParameter<P>(
			int ownerId,
			bool shouldBeInReversedOrder,
			EPortfolioModelSortBy parameter
		)
			where P : CommonPortfolioModel
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

			try
			{
				switch (parameter)
				{
					case EPortfolioModelSortBy.Name:
						return await _commonDbService.GetAllByOwnerSortedByNameAsync<P>(ownerId, shouldBeInReversedOrder);
					case EPortfolioModelSortBy.Value:
						return await _commonDbService.GetAllByOwnerSortedByValueAsync<P>(ownerId, shouldBeInReversedOrder);
					case EPortfolioModelSortBy.Date:
						return await _commonDbService.GetAllByOwnerSortedByDateAsync<P>(ownerId, shouldBeInReversedOrder);
					default:
						throw new ArgumentException(nameof(parameter));
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<P>> GetCommonPortfolioEntitiesByCategory<P>(int ownerId, int categoryId)
			where P : CommonPortfolioModel
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));
			if (categoryId == 0)
				throw new ArgumentException(nameof(categoryId));

			try
			{
				return await _commonDbService.GetAllByCategory<P>(ownerId, categoryId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
