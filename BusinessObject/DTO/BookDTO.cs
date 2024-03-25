using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.DTO;

public class BookDTO
{
    [Required, StringLength(200)] public string BookName { get; set; }
    [Required, StringLength(200)] public string Author { get; set; }
    [Required, Precision(4, 2)] public decimal CostPrice { get; set; }
    [Required, Precision(4, 2)] public decimal SellPrice { get; set; }
    [Required, ForeignKey("CategoryId")] public int CategoryId { get; set; }
    [StringLength(255)] public string Description { get; set; }
    [Required, ForeignKey("PublisherId")] public int PublisherId { get; set; }

    [StringLength(255), DataType(DataType.ImageUrl)]
    public string ImageUrl { get; set; } = "default_product.png";
}

public class ListBooksResponseDTO
{
    public List<Book>? Books { get; set; }
    public int Total { get; set; }
}