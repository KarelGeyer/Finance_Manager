using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ProductModels.Loans
{
    public class CreateLoan
    {
        public int OwnerId { get; set; }

        public int OwnToId { get; set; }

        public float Value { get; set; }

        public string Name { get; set; }
    }
}
