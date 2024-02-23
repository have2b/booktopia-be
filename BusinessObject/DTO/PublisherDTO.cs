using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO;

public class PublisherDTO
{
    [Required, StringLength(100)] public string Name { get; set; }
    [Required, StringLength(20)] public string ContactNumber { get; set; }
}