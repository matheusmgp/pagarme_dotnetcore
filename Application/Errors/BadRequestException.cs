
using System.Net;

namespace Application.Errors
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, null, HttpStatusCode.BadRequest) { }
    }
}
