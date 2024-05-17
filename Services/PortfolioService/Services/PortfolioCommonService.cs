using Azure.Core;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models.PortfolioModels;
using PortfolioService.Helpers;
using PortfolioService.Interfaces;

namespace PortfolioService.Services
{
	public class PortfolioCommonService<T> : IPortfolioCommonService<T>
		where T : PortfolioModel
	{
		private readonly CommonDbService<T> _commonDbService;
		private readonly IValidation<T> _validation;

		public PortfolioCommonService(CommonDbService<T> commonDbService, IValidation<T> validation)
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

			int monthToFilter = month == 0 ? DateTime.Now.Month : month;
			int yearToFilter = year == 0 ? DateTime.Now.Year : year;

			try
			{
				return await _commonDbService.GetAllByDateAsync(ownerId, monthToFilter, yearToFilter);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
