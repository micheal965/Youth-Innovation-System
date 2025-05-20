namespace Youth_Innovation_System.Shared.ApiResponses
{
    public class Response<T> : ApiResponse
    {
        public T? Value { get; set; }
        public string? Details { get; set; }
        public Response(T? value, int statusCode, string? message = null, string? Details = null)
            : base(statusCode, message)
        {
            Value = value;
        }
    }
}
