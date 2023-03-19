using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Category : AuditableBaseEntity
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}
