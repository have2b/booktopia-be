using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRole.Admin)]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoriesController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] RequestDTO input)
    {
        try
        {
            var categories = await _repository.GetCategories(input);
            if (!categories.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() {Code = 404, Message = "There is no data in DB"}
                });
            }

            return Ok(new ResponseDTO<Category[]>()
            {
                Payload = categories.ToArray()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _repository.GetCategoryById(id);
            return Ok(new ResponseDTO<Category>()
            {
                Payload = category
            });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Category with id:{id} not found" }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CategoryDTO model)
    {
        try
        {
            var createdCategory = await _repository.AddCategory(model);

            return CreatedAtAction(nameof(Post), new ResponseDTO<Category>()
            {
                Payload = createdCategory
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO model)
    {
        try
        {
            var updatedCategory = await _repository.UpdateCategory(id, model);

            return Ok(new ResponseDTO<Category>()
            {
                Payload = updatedCategory
            });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Category with id:{id} not found" }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deletedCategory = await _repository.DeleteCategory(id);

            return Ok(new ResponseDTO<Category>()
            {
                Payload = deletedCategory
            });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Category with id:{id} not found" }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }
}