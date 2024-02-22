using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO;

public class CategoryDTO
{
    [Required, StringLength(200)] public string CategoryName { get; set; }
    [StringLength(255)] public string Description { get; set; }
}