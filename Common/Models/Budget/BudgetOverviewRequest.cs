using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Budget
{
    public class BudgetOverviewRequest
    {
        public DateTime For { get; set; }

        public int OwnerId { get; set; }
    }
}
