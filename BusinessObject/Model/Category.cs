using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

[Table("categories")]
public class Category
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int CategoryId { get; set; }

    [Required, StringLength(200)] public string CategoryName { get; set; }
    [StringLength(255)] public string Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}