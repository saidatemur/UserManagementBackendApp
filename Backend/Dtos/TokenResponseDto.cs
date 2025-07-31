namespace UserManagementApp.Dtos
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }
}
