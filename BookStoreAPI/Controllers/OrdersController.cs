using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Get([FromQuery] RequestDTO input, [FromQuery] bool? latest)
    {
        try
        {
            var orders = await _repository.GetOrders(input, latest);
            if (!orders.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No orders found." }
                });
            }

            return Ok(new ResponseDTO<OrderInfoDTO[]>() { Payload = orders.ToArray() });
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

    [HttpGet("user")]
    public async Task<IActionResult> Get()
    {
        string userName = User.FindFirst("username")?.Value;
        try
        {
            var orders = await _repository.GetOrdersByUserId(userName);
            if (!orders.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No orders found." }
                });
            }

            return Ok(new ResponseDTO<OrderInfoDTO[]>() { Payload = orders.ToArray() });
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
        string userName = User.FindFirst("username")?.Value;
        try
        {
            order = await _repository.AddOrder(userName, model);
            return Ok(new ResponseDTO<string>() { Payload = "Create Order successfully" });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"User {userName} not found." }
            });
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

    [HttpGet("{orderId}/OrderDetails")]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
    {
        try
        {
            var orderDetails = await _repository.GetOrderDetailByOrderId(orderId);
            if (!orderDetails.Any())
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "No orders found." }
                });
            }

            return Ok(new ResponseDTO<OrderDetailInfoDTO[]>() { Payload = orderDetails.ToArray() });
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

    [HttpGet("{orderId}")]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> GetOrderByOrderId(int orderId)
    {
        try
        {
            var order = await _repository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = "Not found." }
                });
            }

            return Ok(new ResponseDTO<OrderInfoDTO>() { Payload = order });
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

    [HttpPut("{orderId}/status")]
    public async Task<IActionResult> updateOrderStatus(int orderId, [FromBody] OrderChangeStatusDTO model)
    {
        try
        {
            var order = await _repository.UpdateOrderStatus(orderId, model.Status);
            return Ok(new ResponseDTO<OrderInfoDTO>() { Payload = order });
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 404, Message = $"Order#{orderId} was not found." }
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