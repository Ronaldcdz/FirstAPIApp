using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Category : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
