using Common.Helpers;
using System;

namespace Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when failed to create an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object that failed to be created.</typeparam>
    public class FailedToCreateException<T> : Exception
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCreateException{T}"/> class.
        /// </summary>
        public FailedToCreateException()
            : base(CustomResponseMessage.GetFailedToCreateMessage<T>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCreateException{T}"/> class with the specified ID.
        /// </summary>
        /// <param name="id">The ID associated with the failed creation.</param>
        public FailedToCreateException(int id)
            : base(CustomResponseMessage.GetFailedToCreateMessage<T>(id)) { }
    }
}
