using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.UI.Property
{
    public class EditProperty
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Range(0, 10000000)]
        public double Value { get; set; }

        [Range(1, 20)]
        public int CategoryId { get; set; }
    }
}
