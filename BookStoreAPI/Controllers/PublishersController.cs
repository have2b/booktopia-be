using BusinessObject.DTO;
using DataAccess.Exceptions;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IPublisherRepository _repository;

    public PublishersController(IPublisherRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var publishers = await _repository.GetPublishers();
            if (!publishers.Any())
            {
                return NotFound("No publishers found.");
            }

            return Ok(publishers);
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
            var publisher = await _repository.GetPublisherById(id);
            return Ok(publisher);
        }
        catch (PublisherNotFoundException ex)
        {
            return NotFound($"Publisher with id: {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error. Please try again later.");
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
            return StatusCode(500, "Internal server error. Please try again later.");
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
            return NotFound($"Publisher with id: {id} not found.");
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
            var deletedPublisher = await _repository.DeletePublisher(id);

            return Ok(deletedPublisher);
        }
        catch (PublisherNotFoundException ex)
        {
            return NotFound($"Publisher with id: {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}