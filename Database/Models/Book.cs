using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Book : AuditableBaseEntity
    {
        public string ImagePath;
        public string Title;
        public string Description;
        public int Rating;
        public double Price;
        public int Pages;
        public int CategoryId;
        public int AuthorId;

    }
}
