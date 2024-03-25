using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// A base DB model shared across all Base DB models
    /// </summary>
    public class BaseDbModel : BaseModel
    {
        [PrimaryKey("Id", false)]
        public int Id { get; set; }
    }
}
