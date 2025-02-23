using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Identity;

namespace Youth_Innovation_System.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserLoginHistory> userLoginHistories { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {

        }
    }
}
