using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("books")]
public class Book
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int BookId { get; set; }

    [Required, StringLength(200)] public string BookName { get; set; }
    [Required, StringLength(200)] public string Author { get; set; }
    [Required, Precision(4, 2)] public decimal CostPrice { get; set; }
    [Required, Precision(4, 2)] public decimal SellPrice { get; set; }
    [Required] public int Quantity { get; set; } = 0;
    [Required, ForeignKey("CategoryId")] public int CategoryId { get; set; }
    [StringLength(255)] public string Description { get; set; }
    [Required, ForeignKey("PublisherId")] public int PublisherId { get; set; }

    [StringLength(255), DataType(DataType.ImageUrl)]
    public string ImageUrl { get; set; } = "default_product.png";

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModifiedAt { get; set; } = DateTime.Now;

    public virtual Category? Category { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
}