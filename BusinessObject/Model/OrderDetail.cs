using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("order_details")]
public class OrderDetail
{
    [ForeignKey("OrderId")] public int OrderId { get; set; }
    [ForeignKey("ProductId")] public int ProductId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required, Precision(4, 2)] public decimal Price { get; set; }
    
    [JsonIgnore] public virtual Order? Order { get; set; }
    [JsonIgnore] public virtual Product? Product { get; set; }
}