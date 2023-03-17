using Application.Repository;
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
        private readonly AuthorRepository authorRepository;

        public AuthorController(ApplicationDbContext dbContext)
        {
            authorRepository = new(dbContext);
        }



        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Author>>> GetAuthor()
        {
            var authors = await authorRepository.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, authors);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Author>>> GetAuthorById(int id)
        {

            var author = await authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, author);
            }

            else
            {
                return StatusCode(StatusCodes.Status200OK, author);
            }
        }



        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Author>>> AddAuthor(Author author)
        {
            await authorRepository.AddAsync(author);

            return StatusCode(StatusCodes.Status200OK, await authorRepository.GetAllAsync());
        }


        [HttpPut]
        [Route("")]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author request)
        {
            var authorToUpdate = await authorRepository.GetByIdAsync(request.Id);
            if(authorToUpdate == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Author not found");
            }

            else
            {
                authorToUpdate.Name = request.Name;
                authorToUpdate.BornAt = request.BornAt;
                await authorRepository.UpdateAsync(authorToUpdate, request.Id);
            }

            return StatusCode(StatusCodes.Status200OK, await authorRepository.GetAllAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<Author>>> DeleteAuthorById(int id)
        {

            var author = await authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, author);
            }

            else
            {
                await authorRepository.DeleteAsync(author);
                return StatusCode(StatusCodes.Status200OK, await authorRepository.GetAllAsync());
            }
        }

    }
}
