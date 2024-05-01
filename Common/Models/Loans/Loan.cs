using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Loan
{
    public class Loan : BaseDbModel
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public int To { get; set; }

        public float Value { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}