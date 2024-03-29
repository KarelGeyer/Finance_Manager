using Common.Helpers;
using System;

namespace Common.Exceptions
{
    public class FailedToUpdateException<T> : Exception
        where T : class
    {
        public FailedToUpdateException()
            : base(CustomResponseMessage.GetFailedToUpdateMessage<T>()) { }

        public FailedToUpdateException(int id)
            : base(CustomResponseMessage.GetFailedToUpdateMessage<T>(id)) { }
    }
}
