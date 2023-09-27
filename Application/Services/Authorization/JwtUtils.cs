
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Services.Authorization.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private const int EXPIRATION_MINUTES = 10;
        private readonly IConfiguration _configuration;
        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }     

        public int? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Name").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch (Exception e)
            {
                // return null if validation fails
                return null;
            }
        }
      
        public UserAuthenticationResponseDto GenerateJwtToken(UserAuthenticationRequestDto user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

            var token = CreateJwtToken(
               CreateClaims(user),
               CreateSigningCredentials(),
               expiration
           );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserAuthenticationResponseDto(jwt, expiration);
        }

        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
           new JwtSecurityToken(
               _configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
               claims,
               expires: expiration,
               signingCredentials: credentials
           );
      
        private Claim[] CreateClaims(UserAuthenticationRequestDto user) =>
           new[] {
                new Claim(ClaimTypes.Name, user.UserName),
           };

        private SigningCredentials CreateSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Secret));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

    }
}