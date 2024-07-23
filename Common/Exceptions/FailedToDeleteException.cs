using System;
using Common.Helpers;

namespace Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when failed to delete an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object that failed to be deleted.</typeparam>
    public class FailedToDeleteException<T> : Exception
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToDeleteException{T}"/> class.
        /// </summary>
        public FailedToDeleteException()
            : base(CustomResponseMessage.GetFailedToDeleteMessage<T>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToDeleteException{T}"/> class with the specified ID.
        /// </summary>
        /// <param name="id">The ID associated with the failed deletion.</param>
        public FailedToDeleteException(int id)
            : base(CustomResponseMessage.GetFailedToDeleteMessage<T>(id)) { }
    }
}
