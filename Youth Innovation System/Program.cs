
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Youth_Innovation_System.API.Errors;
using Youth_Innovation_System.API.Middlewares;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Extensions;
using Youth_Innovation_System.Repository.Identity;

namespace Youth_Innovation_System
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //add identityservices
            await builder.Services.AddIdentityServices(builder.Configuration);
            //add applicationservices
            await builder.Services.AddAppServices();

            var app = builder.Build();
            //Migrate and Seeding data for the first time
            await Migrate_SeedingData.Migrate_Seed(app);

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
