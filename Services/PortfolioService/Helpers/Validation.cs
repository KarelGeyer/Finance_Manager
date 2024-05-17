using Common.Interfaces;
using PortfolioService.Interfaces;

namespace PortfolioService.Helpers
{
	public class Validation<T> : IValidation<T>
	{
		// <inheritdoc />
		public void ValidatePortfolioModel(T model)
		{
			if (model is ICommonPortfolioModel)
			{
				ICommonPortfolioModel modelToValidate = (ICommonPortfolioModel)model;
				ValidateCommonPortfolioModel(modelToValidate);
			}

			if (model is IBudget)
			{
				IBudget modelToValidate = (IBudget)model;
				ValidateBudget(modelToValidate);
			}

			if (model is ILoan)
			{
				ILoan modelToValidate = (ILoan)model;
				ValidateLoan(modelToValidate);
			}

			if (model is ISavings)
			{
				ISavings modelToValidate = (ISavings)model;
				ValidateSavings(modelToValidate);
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
		public void ValidateBudget(IBudget budget)
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
		public void ValidateLoan(ILoan loan)
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
		public void ValidateSavings(ISavings savings)
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
