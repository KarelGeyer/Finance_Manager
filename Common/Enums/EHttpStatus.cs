﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    /// <summary>
    ///  Enumarator Representation of an HTTP Status used in the project
    /// </summary>
    public enum EHttpStatus
    {
        OK = 200,
        CREATED = 201,
        ACCEPTED = 202,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        NOT_FOUND = 404,
        INTERNAL_SERVER_ERROR = 500,
        SERVICE_UNAVAILABLE = 503
    }
}
