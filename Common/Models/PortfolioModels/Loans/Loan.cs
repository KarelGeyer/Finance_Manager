using Common.Models.PortfolioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ProductModels.Loans
{
    public class Loan : PortfolioModel
    {
        public string Name { get; set; }

        public int To { get; set; }

        public float Value { get; set; }
    }
}
