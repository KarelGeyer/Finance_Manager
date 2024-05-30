using Common.Interfaces;
using PortfolioService.Interfaces;

namespace PortfolioService.Helpers
{
	public class Validation<T> : IValidation<T>
	{
		// <inheritdoc />
		public void ValidatePortfolioModel(T model)
		{
			switch (model)
			{
				case ICommonPortfolioModel:
					ValidateCommonPortfolioModel((ICommonPortfolioModel)model);
					break;

				case IBudget:
					ValidateBudget((IBudget)model);
					break;

				case ILoan:
					ValidateLoan((ILoan)model);
					break;

				case ISavings:
					ValidateSavings((ISavings)model);
					break;

				default:
					throw new ArgumentException("Invalid model type.");
			}
		}

		/// <summary>
		/// Validate the <see cref="ICommonPortfolioModel"/> model.
		/// </summary>
		/// <param name="model"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private void ValidateCommonPortfolioModel(ICommonPortfolioModel model)
		{
			if (model == null)
				throw new ArgumentNullException();
			if (model.CategoryId == 0)
				throw new ArgumentException("CategoryId");
			if (model.OwnerId == 0)
				throw new ArgumentException("OwnerId");
			if (string.IsNullOrEmpty(model.Name))
				throw new ArgumentException("Name");
			if (model.Value == 0)
				throw new ArgumentException("Value");
		}

		/// <summary>
		/// Validate the <see cref="IBudget"/> model.
		/// </summary>
		/// <param name="budget"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private void ValidateBudget(IBudget budget)
		{
			if (budget == null)
				throw new ArgumentNullException();
			if (budget.CategoryId == 0)
				throw new ArgumentException("CategoryId");
			if (budget.OwnerId == 0)
				throw new ArgumentException("OwnerId");
		}

		/// <summary>
		/// Validate the <see cref="ILoan"/> model.
		/// </summary>
		/// <param name="loan"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private void ValidateLoan(ILoan loan)
		{
			if (loan == null)
				throw new ArgumentNullException();
			if (loan.ToPerson == 0)
				throw new ArgumentException("ToPerson");
			if (loan.OwnerId == 0)
				throw new ArgumentException("OwnerId");
			if (string.IsNullOrEmpty(loan.Name))
				throw new ArgumentException("Name");
			if (loan.Value == 0)
				throw new ArgumentException("Value");
		}

		/// <summary>
		/// Validate the <see cref="ISavings"/> model.
		/// </summary>
		/// <param name="savings"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private void ValidateSavings(ISavings savings)
		{
			if (savings == null)
				throw new ArgumentNullException();
			if (savings.OwnerId == 0)
				throw new ArgumentException("OwnerId");
			if (savings.Amount == 0)
				throw new ArgumentException("Amount");
		}
	}
}
