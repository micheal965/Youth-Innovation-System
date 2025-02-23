using System.Net;
<<<<<<< HEAD

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
                _ => "An error occurred"
            };
        }
    }
}
=======

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
				_ => "An error occurred"
			};
		}
	}
}
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f
