using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("order_details")]
public class OrderDetail
{
    [ForeignKey("OrderId")] public int OrderId { get; set; }
    [ForeignKey("BookId")] public int BookId { get; set; }
    [Required] public int Quantity { get; set; } = 1;

    [Required, Range(0, 99)] public int Discount { get; set; }

    public virtual Order? Order { get; set; }
    public virtual Book? Book { get; set; }
}