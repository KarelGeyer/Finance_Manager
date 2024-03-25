using Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response
{
    /// <summary>
    /// Unified response model
    /// </summary>
    /// <typeparam name="T">A Response Data represetation</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// A model used to represent response data
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// An HTTP status
        /// </summary>
        public EHttpStatus Status { get; set; } = EHttpStatus.OK;

        /// <summary>
        /// Message used to sent a information of the server response
        /// </summary>
        public string ResponseMessage { get; set; } = string.Empty;
    }
}
