using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Threading.RateLimiting;
using Youth_Innovation_System.API.Middlewares;
using Youth_Innovation_System.Extensions;
using Youth_Innovation_System.Middlewares;
using Youth_Innovation_System.Repository.Data;
using Youth_Innovation_System.Repository.Identity;
using Youth_Innovation_System.Service.Hubs;
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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

            //add identityservices
            await builder.Services.AddIdentityServices(builder.Configuration);
            //add applicationservices

            await builder.Services.AddAppServices();

            //Defend from DDos
            builder.Services.AddRateLimiter(options =>
            {
                options.AddPolicy("ip-policy", context =>
                {
                    var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    Console.WriteLine($">> Incoming IP: {ipAddress}");
                    return RateLimitPartition.GetFixedWindowLimiter(ipAddress, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromSeconds(1),
                        QueueLimit = 2,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    });
                });
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("Too many requests", token);
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            var app = builder.Build();
            //Migrate and Seeding data for the first time
            await Migrate_SeedingData.Migrate_Seed(app);

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TokenBlacklistMiddleware>();
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            //if (app.Environment.IsDevelopment())
            //{
            //}
            app.UseRateLimiter();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<ChatHub>("/Hubs/ChatHub");
            app.MapControllers();

            app.Run();
        }
    }
}
