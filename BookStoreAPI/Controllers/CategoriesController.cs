using BusinessObject.DTO;
using DataAccess.Exceptions;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _repository.GetCategories();
                if (!categories.Any())
                {
                    return NotFound("No categories found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _repository.GetCategoryById(id);
                return Ok(category);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound($"Category with id: {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO model)
        {
            try
            {
                var createdCategory = await _repository.AddCategory(model);

                return CreatedAtAction(nameof(Get), createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO model)
        {
            try
            {
                var updatedCategory = await _repository.UpdateCategory(id, model);

                return Ok(updatedCategory);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound($"Category with id: {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedCategory = await _repository.DeleteCategory(id);

                return Ok(deletedCategory);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound($"Category with id: {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}