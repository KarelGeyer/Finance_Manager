using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CustomResponseMessage
    {
        public static string GetNotFoundResponseMessage(string model) =>
            $"No record of type {nameof(model)} was found";
    }
}
