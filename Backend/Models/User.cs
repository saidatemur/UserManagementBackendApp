using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public bool IsBlocked { get; set; } = false;
        
        public DateTime? LastLogin { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        
        public string? ResetToken { get; set; }
        
        public DateTime? ResetTokenExpiry { get; set; }
    }
}