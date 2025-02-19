namespace Youth_Innovation_System.Shared.ApiResponses
{
	public class ApiExceptionResponse : ApiResponse
	{
		public string? Details { get; set; }
		public ApiExceptionResponse(int statuscode, string message, string? details = null)
			: base(statuscode, message)
		{
			Details = details;
		}
	}
}