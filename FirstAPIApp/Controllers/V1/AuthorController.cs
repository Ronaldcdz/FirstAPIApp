using Database;
using Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FirstAPIApp.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        private static List<Author> authors = new List<Author>
            {
                new Author
                {
                    Id= 1,
                    Name = "James Clear",
                    BornAt = "26/02/2002"
                }
            };


        public AuthorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<List<Author>>> Get()
        {
            

            return StatusCode(StatusCodes.Status200OK, authors);
            //return Ok(authors);
        }
    }
}
