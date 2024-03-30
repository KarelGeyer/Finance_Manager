using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Income
{
    /// <summary>
    /// Represents a request to update the name of an income.
    /// </summary>
    public class IncomeUpdateNameRequest
    {
        /// <summary>
        /// Gets or sets the ID of the income.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new name for the income.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents a request to update the value of an income.
    /// </summary>
    public class IncomeUpdateValueRequest
    {
        /// <summary>
        /// Gets or sets the ID of the income.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new value for the income.
        /// </summary>
        public float Value { get; set; }
    }
}
