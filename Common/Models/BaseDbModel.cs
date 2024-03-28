using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Common.Models
{
    /// <summary>
    /// A base DB model shared across all Base DB models
    /// </summary>
    public class BaseDbModel : BaseModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }
    }
}
