using Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }

        public EHttpStatus Status { get; set; } = EHttpStatus.OK;

        public string ResponseMessage { get; set; } = string.Empty;
    }
}
