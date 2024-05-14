using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ProductModels.Properties
{
    public class CreateProperty
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int CategoryId { get; set; }
    }
}
