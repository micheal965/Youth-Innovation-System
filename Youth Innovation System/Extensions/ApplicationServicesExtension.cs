using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Service;
using Youth_Innovation_System.Shared.ApiResponses;

namespace Youth_Innovation_System.Extensions
{
    public static class ApplicationServicesExtension
    {
        public async static Task AddAppServices(this IServiceCollection Services)
        {
            Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddScoped<IPasswordService, PasswordService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IUserVerificationService, UserVerificationService>();

            Services.AddTransient<IEmailService, EmailService>();

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    //key value =>error.count>0
                    //value.errors
                    //errors.errormessage

                    //key value  =>error.count=0
                    //key value  =>error.count=0

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
