using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.PortfolioModels.Budget
{
    public class CreateBudget
    {
        public int Parent { get; set; }

        public int CategoryId { get; set; }

        public float Value { get; set; }
    }
}
