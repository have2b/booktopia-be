using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("orders")]
public class Order
{
    public enum StatusType
    {
        Processing,
        Delivering,
        Shipped,
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int OrderId { get; set; }

    [Required, ForeignKey("UserId")] public string UserId { get; set; }

    public StatusType Status { get; set; } = StatusType.Processing;

    [Required, StringLength(200)] public string? ShipAddress { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
}