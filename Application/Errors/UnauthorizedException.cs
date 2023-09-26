


using System.Net;

namespace Application.Errors
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message)
            : base(message, null, HttpStatusCode.Unauthorized) { }
    }
}
