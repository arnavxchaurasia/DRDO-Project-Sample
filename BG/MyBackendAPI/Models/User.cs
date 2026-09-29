using System.ComponentModel.DataAnnotations;

namespace MyBackendAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // Deliberately a plain string rather than an enum-backed column: new
        // roles can be introduced without a migration, and RBAC checks below
        // only ever compare against "Admin" today.
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "Admin";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
