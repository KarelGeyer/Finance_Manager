using Common.Helpers;
using System;

namespace Common.Exceptions
{
    public class FailedToDeleteException<T> : Exception
        where T : class
    {
        public FailedToDeleteException()
            : base(CustomResponseMessage.GetFailedToDeleteMessage<T>()) { }

        public FailedToDeleteException(int id)
            : base(CustomResponseMessage.GetFailedToDeleteMessage<T>(id)) { }
    }
}
