using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.DTO;

public class OrderDetailDTO
{
    [ForeignKey("OrderId")] public int OrderId { get; set; }
    [ForeignKey("BookId")] public int BookId { get; set; }
    [Required] public int Quantity { get; set; } = 1;
    [Required, Range(0, 99)] public int Discount { get; set; }
}

public class OrderDetailInfoDTO
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public string? BookName { get; set; }
    public string? Author { get; set; }
    public string? PublisherName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal SellPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
}