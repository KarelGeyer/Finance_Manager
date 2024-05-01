using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Savings
{
    public class Savings : BaseDbModel
    {
        public double Amount { get; set; }

        public int OwnerId { get; set; }
    }
}