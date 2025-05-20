using System.Net;

namespace Youth_Innovation_System.Shared.ApiResponses
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageForStatusCode((HttpStatusCode)statusCode);
        }

        private string? GetMessageForStatusCode(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad Request",
                HttpStatusCode.Unauthorized => "Unauthorized",
                HttpStatusCode.Forbidden => "Forbidden",
                HttpStatusCode.NotFound => "Not Found",
                HttpStatusCode.InternalServerError => "Internal Server Error",
                HttpStatusCode.OK => "OK",
                _ => "An error occurred"
            };
        }
    }
}
