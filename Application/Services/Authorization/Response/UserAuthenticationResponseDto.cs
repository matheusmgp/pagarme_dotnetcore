namespace Application.Services.Authorization.Response
{
    public class UserAuthenticationResponseDto
    {
        public UserAuthenticationResponseDto(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
