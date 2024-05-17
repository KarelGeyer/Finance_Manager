using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Loans;
using Common.Models.ProductModels.Properties;
using Common.Models.Savings;

namespace Common.Validations
{
	public static class PortfolioServiceValidations
	{
		/// TODO #10001
		/// Task: Check what kind of categories and ids are valid for each entity and adjust the categoryId validation
		/// to be withing this certain range of categories... That is to avoid budget being created with category that is only
		/// valid for Expense entity
		/// Date: 17-05-2024
		/// Importance: High
		/// ID: #10001-17-05-2024-High
	}
}
