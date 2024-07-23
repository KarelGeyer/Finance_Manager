using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ILoan
    {
        /// <summary>
        /// Gets or sets the Id of a user who this is owned to of the loan.
        /// 0 means the user is not in the system
        /// </summary>
        public int ToPerson { get; }

        /// <summary>
        /// Gets or sets the value of the loan.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets or sets the name of the loan.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the owner ID of the income.
        /// </summary>
        int OwnerId { get; }
    }
}
