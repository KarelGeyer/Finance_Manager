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
    /// <summary>
    /// Represents a Category Type Entity
    /// </summary>
    [Table("CategoryTypes")]
    public class CategoryType : BaseDbModel
    {
        /// <summary>
        /// Integer representation of a <see cref="CategoryType"/>
        /// </summary>
        [Column("Value")]
        public ECategoryType Value { get; set; }
    }
}
