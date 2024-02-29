using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

    }

    public class UserUpdateInfomationRequest
    {
        public string UserName { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
    }

    public class UserChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class UserSetIsActiveRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
