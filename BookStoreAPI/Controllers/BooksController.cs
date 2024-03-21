using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRole.Admin)]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] RequestDTO input, [FromQuery] bool? latest)
    {
        try
        {
            var books = await _repository.GetBooks(input, latest);
            if (!books.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No books found." }
                });
            }

            return Ok(new ResponseDTO<Book[]>() { Payload = books.ToArray() });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var book = await _repository.GetBookById(id);
            return Ok(new ResponseDTO<Book>() { Payload = book });
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Book with id: {id} not found." }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDTO model)
    {
        try
        {
            var createdBook = await _repository.AddBook(model);

            return CreatedAtAction(nameof(Get), createdBook);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BookDTO model)
    {
        try
        {
            var updatedBook = await _repository.UpdateBook(id, model);

            return Ok(updatedBook);
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Book with id: {id} not found." }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deletedBook = await _repository.DeleteBook(id);

            return Ok(deletedBook);
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Book with id: {id} not found." }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }

    // Import book
    [HttpPost("import")]
    public async Task<IActionResult> Import([FromBody] BookImportDTO model)
    {
        try
        {
            var importedBook = await _repository.ImportBook(model.BookId, model.Quantity);

            return CreatedAtAction(nameof(Post), importedBook);
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Book with id: {model.BookId} not found." }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
            });
        }
    }
}