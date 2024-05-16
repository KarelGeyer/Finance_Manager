namespace Common.Helpers
{
    /// <summary>
    /// A static class containing methods and static strings used to get response messages across all services
    /// </summary>
    public static class CustomResponseMessage
    {
        /// <summary>
        /// Represents a string information of a record not being found
        /// </summary>
        /// <param name="model">A string representation of a model used in a request</param>
        /// <returns>A string response message</returns>
        public static string GetNotFoundResponseMessage(string model) =>
            $"No record of type {model} was found";

        /// <summary>
        /// Represents a string information of a failed creation of a new object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="id">The ID of the object</param>
        /// <returns>A string response message</returns>
        public static string GetFailedToCreateMessage<T>(int id)
            where T : class => $"Failed to create new {nameof(T)} with Id {id}";

        /// <summary>
        /// Represents a string information of a failed creation of a new object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <returns>A string response message</returns>
        public static string GetFailedToCreateMessage<T>()
            where T : class => $"Failed to create new {nameof(T)}";

        /// <summary>
        /// Represents a string information of a failed update of an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="id">The ID of the object</param>
        /// <returns>A string response message</returns>
        public static string GetFailedToUpdateMessage<T>(int id)
            where T : class => $"Failed to update {nameof(T)} with Id {id}";

        /// <summary>
        /// Represents a string information of a failed update of an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <returns>A string response message</returns>
        public static string GetFailedToUpdateMessage<T>()
            where T : class => $"Failed to update {nameof(T)}";

        /// <summary>
        /// Represents a string information of a failed deletion of an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="id">The ID of the object</param>
        /// <returns>A string response message</returns>
        public static string GetFailedToDeleteMessage<T>(int id)
            where T : class => $"Failed to delete {nameof(T)} with Id {id}";

        /// <summary>
        /// Represents a string information of a failed deletion of an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <returns>A string response message</returns>
        public static string GetFailedToDeleteMessage<T>()
            where T : class => $"Failed to delete {nameof(T)}";
    }
}
