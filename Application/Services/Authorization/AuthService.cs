
using Application.Services.Contracts;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Authorization
{
    public class AuthService : IAuthService
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }



    }
}
