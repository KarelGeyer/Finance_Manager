using Common.Enums;
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
    /// Represents a Category DB Entity
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Get or sets the entity Id attribute
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents a <see cref="CategoryType"/>'s Id
        /// </summary>
        public int CategoryTypeId { get; set; }

        /// <summary>
        /// An actual string representation of a <see cref="Category"/>
        /// </summary>
        public string Value { get; set; }
    }
}
