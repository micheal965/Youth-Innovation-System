using System.Net;

namespace Youth_Innovation_System.Shared.ApiResponses
{
	public class ApiValidationErrorsResponse : ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }
		public ApiValidationErrorsResponse()
			: base((int)HttpStatusCode.BadRequest)
		{
			Errors = new List<string>();
		}


	}
}