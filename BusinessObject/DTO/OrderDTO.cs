using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.DTO;

public class OrderDTO
{
    [Required, ForeignKey("UserId")] public string UserId { get; set; }

    [Required, StringLength(200)] public string ShipAddress { get; set; }

    [Required] public List<OrderDetailDTO> OrderDetails { get; set; }
}