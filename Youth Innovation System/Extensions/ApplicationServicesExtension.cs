using Microsoft.AspNetCore.Mvc;
using Youth_Innovation_System.API.Errors;

namespace Youth_Innovation_System.Extensions
{
    public static class ApplicationServicesExtension
    {
        public async static Task AddAppServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    //key value 
                    //value.errors
                    //errors.errormessage
                    //key value 
                    //key value 

                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(p => p.ErrorMessage).ToArray();

                    var apiresponse = new ApiValidationErrorsResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(apiresponse);
                };
            });
        }
    }
}
