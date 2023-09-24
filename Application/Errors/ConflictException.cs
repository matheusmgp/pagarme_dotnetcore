
using System.Net;


namespace Application.Errors
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message)
            : base(message, null, HttpStatusCode.Conflict) { }
    }
}
