using System.ComponentModel.DataAnnotations;
using static BusinessObject.Model.Order;

namespace BusinessObject.DTO;

public class OrderDTO
{
    [Required, StringLength(200)] public string ShipAddress { get; set; }

    [Required] public List<OrderDetailDTO> OrderDetails { get; set; }
}

public class OrderChangeStatusDTO
{
    [Required]
    public StatusType Status { get; set; }

}

public class OrderInfoDTO
{
    public int OrderId { get; set; }
    public StatusType Status { get; set; }
    public string? ShipAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public decimal SaleAmount { get; set; }
}