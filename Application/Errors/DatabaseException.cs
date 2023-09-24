
using System.Net;

namespace Application.Errors
{
    public class DatabaseException : CustomException
    {
        public DatabaseException(string message)
            : base(message, null, HttpStatusCode.ServiceUnavailable) { }
    }
}
