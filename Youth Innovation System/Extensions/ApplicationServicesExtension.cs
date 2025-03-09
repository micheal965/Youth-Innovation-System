using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Helpers;
using Youth_Innovation_System.Repository.Data;
using Youth_Innovation_System.Service;
using Youth_Innovation_System.Service.IdentityServices;
using Youth_Innovation_System.Service.PostServices;
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

            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddScoped<IPasswordService, PasswordService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IUserVerificationService, UserVerificationService>();
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IPostService, PostService>();
            Services.AddTransient<IEmailService, EmailService>();
            Services.AddScoped<ICloudinaryServices, CloudinaryServices>(); // Register the Cloudinary Service


            Services.AddAutoMapper(typeof(MappingProfile));

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
