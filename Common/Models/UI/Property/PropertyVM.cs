using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.UI.Property
{
    public class PropertyVM
    {
        public Common.Models.ProductModels.Properties.Property Property { get; set; }

        public bool ShouldDeletePartialBeVisible { get; set; } = false;
    }
}
