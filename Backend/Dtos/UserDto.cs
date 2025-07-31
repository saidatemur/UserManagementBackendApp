namespace UserManagementApp.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime LastLogin { get; set; }
        public string Status { get; set; } = null!; // "active", "blocked"
        public DateTime RegisteredAt { get; set; }
    }
}
