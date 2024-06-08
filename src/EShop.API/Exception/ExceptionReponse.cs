using System.Net;

namespace EShop.API.Exception
{
    public class ExceptionResponse(HttpStatusCode statusCode, string message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
        public string Message { get; } = message;
    }
}
