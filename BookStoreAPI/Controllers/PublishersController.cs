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
public class PublishersController : ControllerBase
{
    private readonly IPublisherRepository _repository;

    public PublishersController(IPublisherRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] RequestDTO input)
    {
        try
        {
            var publishers = await _repository.GetPublishers(input);
            if (!publishers.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No publishers found." }
                });
            }

            return Ok(new ResponseDTO<Publisher[]>() { Payload = publishers.ToArray() });
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

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var publisher = await _repository.GetPublisherById(id);
            return Ok(new ResponseDTO<Publisher>() { Payload = publisher });
        }
        catch (PublisherNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Publisher with id: {id} not found." }
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
    public async Task<IActionResult> Post([FromBody] PublisherDTO model)
    {
        try
        {
            var createdPublisher = await _repository.AddPublisher(model);

            return CreatedAtAction(nameof(Get), createdPublisher);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PublisherDTO model)
    {
        try
        {
            var updatedPublisher = await _repository.UpdatePublisher(id, model);

            return Ok(updatedPublisher);
        }
        catch (PublisherNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Publisher with id: {id} not found." }
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
            var deletedPublisher = await _repository.DeletePublisher(id);

            return Ok(deletedPublisher);
        }
        catch (PublisherNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Publisher with id: {id} not found." }
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