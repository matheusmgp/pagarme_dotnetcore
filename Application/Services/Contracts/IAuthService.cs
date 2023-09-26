

using Application.Dtos;

namespace Application.Services.Contracts
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);

    }
}
