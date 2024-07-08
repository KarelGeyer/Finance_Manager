using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IBudget
    {
        /// <summary>
        /// Represents a <see cref="Category.Category"/>'s Id
        /// </summary>
        int CategoryId { get; }

        /// <summary>
        /// Represents a <see cref="Budget"/>'s Value
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Gets or sets the owner ID of the income.
        /// </summary>
        int OwnerId { get; }
    }
}
