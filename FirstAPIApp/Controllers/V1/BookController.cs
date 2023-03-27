using Application.Repository;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPIApp.Controllers.V1
{
    [EnableCors("CorsRules")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookRepository bookRepository;
        private readonly List<string> properties = new List<string>() {"Author", "Category"};


        public BookController(ApplicationDbContext dbContext)
        {
            bookRepository = new (dbContext);
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return StatusCode(StatusCodes.Status200OK, await bookRepository.GetAllWithIncludeAsync(properties));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Book Not found");
            }

            else
            {
                return StatusCode(StatusCodes.Status200OK, book);

            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            try
            {
                await bookRepository.AddAsync(book);
                return StatusCode(StatusCodes.Status200OK, await bookRepository.GetAllAsync());
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Error: "+ex.Message);
                
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<List<Book>>> UpdateBook (Book request)
        {
            var bookToUpdate = await bookRepository.GetByIdAsync(request.Id);

            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Book Not found");
            }

            else
            {

                try
                {
                    bookToUpdate.ImagePath = request.ImagePath is null ? bookToUpdate.ImagePath : request.ImagePath;
                    bookToUpdate.Title = request.Title is null ? bookToUpdate.Title : request.Title;
                    bookToUpdate.Description = request.Description is null ? bookToUpdate.Description : request.Description;
                    bookToUpdate.Rating = request.Rating is 0 ? bookToUpdate.Rating : request.Rating;
                    bookToUpdate.Price = request.Price is 0 ? bookToUpdate.Price : request.Price;
                    bookToUpdate.Pages = request.Pages is 0 ? bookToUpdate.Pages : request.Pages;
                    bookToUpdate.Quantity = request.Quantity is 0 ? bookToUpdate.Quantity : request.Quantity;
                    bookToUpdate.CategoryId = request.CategoryId is 0 ? bookToUpdate.CategoryId : request.CategoryId;
                    bookToUpdate.AuthorId = request.AuthorId is 0 ? bookToUpdate.AuthorId : request.AuthorId;


                    await bookRepository.UpdateAsync(request, request.Id);
                    return StatusCode(StatusCodes.Status200OK, request);
                }

                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Error: " + ex.Message);

                }

            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Book>> DeleteBookById(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Book Not found");
            }

            else
            {
                await bookRepository.DeleteAsync(book);
                return StatusCode(StatusCodes.Status200OK, await bookRepository.GetAllAsync());

            }
        }

    }
}
