using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

[Table("users")]
public class User : IdentityUser
{
    [Required, StringLength(200)] 
    public string Name { get; set; }
    [Required, StringLength(255)]
    public string Address { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime LastModifiedAt { get; private set; } = DateTime.Now;


    // Call this method when the user data is updated
    public void UpdateLastModified() => LastModifiedAt = DateTime.Now;
    [JsonIgnore] public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore] public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
}