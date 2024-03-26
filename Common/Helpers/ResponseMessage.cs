using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    /// <summary>
    /// A static class containing methods and static strings used to get response messages across all services
    /// </summary>
    public static class CustomResponseMessage
    {
        /// <summary>
        /// Represens a string information of a record not being found
        /// </summary>
        /// <param name="model">a string representation of a model used in a request</param>
        /// <returns>a string response message</returns>
        public static string GetNotFoundResponseMessage(string model) =>
            $"No record of type {nameof(model)} was found";
    }
}
