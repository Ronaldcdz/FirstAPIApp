using Application.Repository;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPIApp.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository categoryRepository;     // Database Access for Category Table

        public CategoryController(ApplicationDbContext dbContext)
        {
            categoryRepository = new (dbContext);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> GetCategory()
        {
            return StatusCode(StatusCodes.Status200OK, await categoryRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, category);

            }

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> AddCategory(Category category)
        {
            try
            {
                await categoryRepository.AddAsync(category);
                return StatusCode(StatusCodes.Status200OK, await categoryRepository.GetAllAsync());

            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<List<Category>>> UpdateCategory(Category request)
        {
            var categoryToUpdate = await categoryRepository.GetByIdAsync(request.Id);
            
            if(categoryToUpdate == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User not found");
            }

            try
            {
                categoryToUpdate.Title = request.Title is null ? categoryToUpdate.Title : request.Title;
                categoryToUpdate.Description = request.Description is null ? categoryToUpdate.Description : request.Description;

                await categoryRepository.UpdateAsync(categoryToUpdate, request.Id);
                return StatusCode(StatusCodes.Status200OK, await categoryRepository.GetByIdAsync(request.Id));
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Error: "+ex.Message);
            }

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if(category == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User not found");
            }

            else
            {
                await categoryRepository.DeleteAsync(category);
                return StatusCode(StatusCodes.Status200OK, await categoryRepository.GetAllAsync());
            }
        }

    }
}
