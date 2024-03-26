using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Exceptions
{
    /// <summary>
    /// A Custom Exception throwen when a record or a list of records are not found in the database
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base(string.Format("No record was found")) { }

        public NotFoundException(int id)
            : base(string.Format("No record by id {0} was found", id)) { }
    }
}
