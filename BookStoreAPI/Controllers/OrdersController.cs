using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;

    public OrdersController(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> Get([FromQuery] RequestDTO input)
    {
        try
        {
            var orders = await _repository.GetOrders(input);
            if (!orders.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No orders found." }
                });
            }

            return Ok(new ResponseDTO<Order[]>() { Payload = orders.ToArray() });
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
    public async Task<IActionResult> Post([FromBody] OrderDTO model)
    {
        Order order = null;
        try
        {
            order = await _repository.AddOrder(model);
            return Ok(new ResponseDTO<Order>() { Payload = order });
        }
        catch (InsufficientQuantityException ex)
        {
            return StatusCode(500, new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 500, Message = ex.Message }
            });
        }
    }
}