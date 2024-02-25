using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

[Table("publishers")]
public class Publisher
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int PublisherId { get; set; }

    [Required, StringLength(100)] public string Name { get; set; }
    [Required, StringLength(20)] public string ContactNumber { get; set; }
}