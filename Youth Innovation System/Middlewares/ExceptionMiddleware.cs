using System.Net;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //in case of production-> you need to log error in Database
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.Headers.ContentType = "application/json";

            switch (ex)
            {
                case ArgumentNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case KeyNotFoundException or NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                // Add as many types as needed
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = env.IsDevelopment() ?
               new Response<object>(null, context.Response.StatusCode, ex.Message, ex.StackTrace.ToString()) :
               new Response<object>(null, context.Response.StatusCode, ex.Message);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}

