

namespace Application.Dtos
{
    public class UserAuthenticationRequestDto
    {
        public required string UserName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}