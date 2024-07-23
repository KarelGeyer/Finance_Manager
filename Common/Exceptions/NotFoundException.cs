using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Exceptions
{
    /// <summary>
    /// A Custom Exception thrown when a record or a list of records are not found in the database
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
            : base(string.Format("No record was found")) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with the specified id.
        /// </summary>
        /// <param name="id">The id of the record.</param>
        public NotFoundException(int id)
            : base(string.Format("No record by id {0} was found", id)) { }
    }
}
