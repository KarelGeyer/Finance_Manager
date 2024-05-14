using Common.Models.PortfolioModels;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Savings
{
    public class Savings : PortfolioModel
    {
        public double Amount { get; set; }
    }
}
