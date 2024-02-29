using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO;

public class OrderDTO
{
    [Required, StringLength(200)] public string ShipAddress { get; set; }

    [Required] public List<OrderDetailDTO> OrderDetails { get; set; }
}