﻿using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Request
{
    public class BaseRequest<T>
    {
        public T? Data { get; set; }
    }
}