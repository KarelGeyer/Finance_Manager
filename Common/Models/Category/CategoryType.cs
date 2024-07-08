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
    public class CategoryType
    {
        /// <summary>
        /// Get or sets the entity Id attribute
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Integer representation of a <see cref="CategoryType"/>
        /// </summary>
        public string Value { get; set; }
    }
}
