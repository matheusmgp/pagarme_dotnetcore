

using Application.Dtos;
using Application.Services.Authorization.Response;

namespace Application.Services.Authorization
{
    public interface IJwtUtils
    {
        UserAuthenticationResponseDto GenerateJwtToken(UserAuthenticationRequestDto user);
        int? ValidateJwtToken(string? token);
    }
}