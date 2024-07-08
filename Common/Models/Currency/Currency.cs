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
    public class Currency
    {
        /// <summary>
        /// Get or sets the entity Id attribute
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the currency.
        /// </summary>
        public string Value { get; set; }
    }
}
