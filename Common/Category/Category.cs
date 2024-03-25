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
    /// Represents a Category DB Entity
    /// </summary>
    [Table("Categories")]
    public class Category : BaseDbModel
    {
        /// <summary>
        /// Represents a <see cref="CategoryType"/>'s Id
        /// </summary>
        [Column("Type")]
        public int Type { get; set; }

        /// <summary>
        /// An actual string representation of a <see cref="Category"/>
        /// </summary>
        [Column("Value")]
        public string Value { get; set; }
    }
}
