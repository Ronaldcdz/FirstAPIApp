using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Author : AuditableBaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string? BornAt { get; set; }


        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}
