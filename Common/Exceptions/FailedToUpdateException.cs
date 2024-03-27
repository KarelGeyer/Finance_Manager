using Common.Helpers;
using System;

namespace Common.Exceptions
{
    public class FailedToCreateException<T> : Exception
        where T : class
    {
        public FailedToCreateException()
            : base(CustomResponseMessage.GetFailedToCreateMessage<T>()) { }

        public FailedToCreateException(int id)
            : base(CustomResponseMessage.GetFailedToCreateMessage<T>(id)) { }
    }
}
