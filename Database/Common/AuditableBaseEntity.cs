using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common
{
    public class AuditableBaseEntity
    {
        public int Id;
        public DateTime CreatedAt;
        public DateTime IpdatedAt;



    }
}
