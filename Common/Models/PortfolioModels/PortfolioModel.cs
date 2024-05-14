using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.PortfolioModels
{
    public class PortfolioModel : BaseDbModel
    {
        /// <summary>
        /// Gets or sets the owner ID of the income.
        /// </summary>
        public int OwnerId { get; set; }
    }
}
