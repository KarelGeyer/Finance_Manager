using Common.Enums;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Category
{
    /// <summary>
    /// Represents a Category Type Entity
    /// </summary>
    [Table("CategoriesTypes")]
    public class CategoryType : BaseDbModel
    {
        /// <summary>
        /// Integer representation of a <see cref="CategoryType"/>
        /// </summary>
        [Column("Value")]
        public string Value { get; set; }
    }
}
