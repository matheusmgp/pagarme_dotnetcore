

using Application.Dtos;

namespace Application.Services.Authorization
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(UserDto user);
        int? ValidateJwtToken(string? token);
    }
}