using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository (ApplicationDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
