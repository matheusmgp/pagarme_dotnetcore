
using System.Net;

namespace Application.Errors
{
    internal class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, null, HttpStatusCode.BadRequest) { }
    }
}
