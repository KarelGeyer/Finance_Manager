using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.PortfolioModels.Budget
{
    public class Budget : PortfolioModel
    {
        /// <summary>
        /// An id representation of a <see cref="BudgetOverview"/>
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// Represents a <see cref="Category.Category"/>'s Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Represents a <see cref="Budget"/>'s Value
        /// </summary>
        public float Value { get; set; }
    }
}
