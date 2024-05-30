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

		public PortfolioCommonService(IDbService<T> dbService)
		{
			_dbService = dbService;
		}

		/// <inheritdoc />
		public List<T> GetCommonPortfolioEntitiesSortedByGivenParameter(int ownerId, bool shouldBeInReversedOrder, EPortfolioModelSortBy parameter)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));

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
						throw new ArgumentException(nameof(parameter));
				}

				if (shouldBeInReversedOrder)
					entities.Reverse();

				return entities;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <inheritdoc />
		public async Task<List<T>> GetCommonPortfolioEntitiesByCategory(int ownerId, int categoryId)
		{
			if (ownerId == 0)
				throw new ArgumentException(nameof(ownerId));
			if (categoryId == 0)
				throw new ArgumentException(nameof(categoryId));

			try
			{
				return await _dbService.GetAll(x => x.OwnerId == ownerId && x.CategoryId == categoryId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
