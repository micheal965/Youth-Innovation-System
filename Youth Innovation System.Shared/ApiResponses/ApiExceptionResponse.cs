<<<<<<< HEAD
﻿
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
=======
﻿namespace Youth_Innovation_System.Shared.ApiResponses
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
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f
