using System.Net;

namespace EShop.API.Exception
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ExceptionResponse(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
