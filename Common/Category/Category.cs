using Common.Enums;
using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Category
{
    [Table("Categories")]
    public class Category : BaseModel
    {
        [PrimaryKey("Id", false)]
        public int Id { get; set; }

        [Column("Type")]
        public ECategoryType Type { get; set; }

        [Column("Value")]
        public string Value { get; set; }
    }
}
