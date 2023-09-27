namespace Application.Services.Authorization.Response
{
    public class UserAuthenticationFailResponse
    {
        public UserAuthenticationFailResponse(string message)
        {
            Message = message;
        }
        public string Message { get; set; }

    }
}
