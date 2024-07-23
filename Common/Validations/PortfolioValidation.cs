using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models.Expenses;
using Common.Models.PortfolioModels;
using Common.Models.PortfolioModels.Budget;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Loans;
using Common.Models.ProductModels.Properties;
using Common.Models.Savings;

namespace Common.Validations
{
    public class PortfolioValidation<T> : IPortfolioValidation<T>
        where T : PortfolioModel
    {
        /// TODO #10001
        /// Task: Check what kind of categories and ids are valid for each entity and adjust the categoryId validation
        /// to be withing this certain range of categories... That is to avoid budget being created with category that is only
        /// valid for Expense entity
        /// Date: 17-05-2024
        /// Importance: High
        /// ID: #10001-17-05-2024-High

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
