using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Currency
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public class Currency : BaseDbModel
    {
        /// <summary>
        /// Gets or sets the value of the currency.
        /// </summary>
        public string Value { get; set; }
    }
}
