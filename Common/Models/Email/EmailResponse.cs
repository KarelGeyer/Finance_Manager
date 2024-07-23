using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Email
{
    public class EmailResponse
    {
        /// <summary>
        /// Represents the status of the email sending process
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Represents the message of the email sending process
        /// </summary>
        public string Message { get; set; }
    }
}
