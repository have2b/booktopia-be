using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

[Table("users")]
public class User
{
    public enum UserRole
    {
        User,
        Admin,
        // Add other roles as needed
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int UserId { get; set; }

    [Required, StringLength(20)] public string Username { get; set; }

    [JsonIgnore]
    [Required, StringLength(64)] // Assuming SHA-256 for password hashing
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;
    [Required, StringLength(200)] public string Name { get; set; }

    [Required, StringLength(200), EmailAddress]
    public string Email { get; set; }

    [Required, StringLength(255)] public string Address { get; set; }
    [StringLength(11)] public string Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime LastModifiedAt { get; private set; } = DateTime.Now;

    // Call this method when the user data is updated
    public void UpdateLastModified() => LastModifiedAt = DateTime.Now;


    [JsonIgnore] public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore] public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
}