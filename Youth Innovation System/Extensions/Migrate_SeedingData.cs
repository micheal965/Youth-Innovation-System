using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Repository.Identity;

namespace Youth_Innovation_System.Extensions
{
    public static class Migrate_SeedingData
    {
        public async static Task Migrate_Seed(WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var services = scope.ServiceProvider;

            var loggerfactory = services.GetRequiredService<ILoggerFactory>();

            // var _dbcontext = services.GetRequiredService<ApplicationDbContext>();
            var _Identitydbcontext = services.GetRequiredService<AppIdentityDbContext>();

            try
            {
                //Add migrations
                //await _dbcontext.Database.MigrateAsync();
                await _Identitydbcontext.Database.MigrateAsync();

                //seeding Roles
                await services.SeedRoles();
                //seeding admin
                var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await usermanager.SeedAdmin();
                //await ApplicationContextSeed.DataSeed(_dbcontext);
                //var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();
                //await AppIdentityDbContextSeed.SeedUsersAsync(usermanager);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying the migrations");

            }
        }
    }
}
