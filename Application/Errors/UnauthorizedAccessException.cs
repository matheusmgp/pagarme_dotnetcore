


using System.Net;

namespace Application.Errors
{
    internal class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message)
            : base(message, null, HttpStatusCode.Unauthorized) { }
    }
}
