using System.ComponentModel.DataAnnotations;

namespace HMS_API.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int RoleId { get; set; }

    }
}
