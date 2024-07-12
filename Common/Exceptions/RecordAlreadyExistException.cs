using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class RecordAlreadyExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCreateException{T}"/> class.
        /// </summary>
        public RecordAlreadyExistException()
            : base(CustomResponseMessage.GetRecordAlreadyExistsMessage()) { }
    }
}
