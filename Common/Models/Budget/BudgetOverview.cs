using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Budget
{
    public class BudgetOverview
    {
        /// <summary>
        /// Represents a <see cref="CategoryType"/>'s Id
        /// </summary>
        public int Id { get; set; }

        public DateTime For { get; set; }

        public int OwnerId { get; set; }
    }
}
