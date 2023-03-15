using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Author : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string? BornAt { get; set; }


        public ICollection<Book>? Books { get; set; }
    }
}
