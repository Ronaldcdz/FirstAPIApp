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
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public int Pages { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public Author? Author { get; set; } 
        public Category? Category { get; set; } 
    }
}
