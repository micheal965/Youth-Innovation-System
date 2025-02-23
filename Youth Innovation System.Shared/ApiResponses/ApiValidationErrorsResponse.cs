using System.Net;

namespace Youth_Innovation_System.Shared.ApiResponses
{
<<<<<<< HEAD
    public class ApiValidationErrorsResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorsResponse()
            : base((int)HttpStatusCode.BadRequest)
        {
            Errors = new List<string>();
        }
=======
	public class ApiValidationErrorsResponse : ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }
		public ApiValidationErrorsResponse()
			: base((int)HttpStatusCode.BadRequest)
		{
			Errors = new List<string>();
		}
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f


	}
}