using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Repository.Identity;

namespace Youth_Innovation_System.Service
{
    public class UserService : IUserService
    {
        private readonly AppIdentityDbContext _appIdentityDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppIdentityDbContext appIdentityDbContext,
                            UserManager<ApplicationUser> userManager,
                            IHttpContextAccessor httpContextAccessor)
        {
            _appIdentityDbContext = appIdentityDbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IReadOnlyList<UserLoginHistory>> GetLoginHistory(string userId)
        {
            return await _appIdentityDbContext.userLoginHistories
                .Where(l => l.ApplicationUserId == userId)
                .OrderByDescending(l => l.LoginTime).ToListAsync();
        }
        public async Task SaveLoginAttempt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

                // Check if the app is behind a proxy (e.g., Nginx, Cloudflare)
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    ipAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0].Trim();
                }

                ipAddress = ipAddress == "::1" ? "127.0.0.1" : ipAddress; // Convert ::1 to 127.0.0.1 if local
                await _appIdentityDbContext.userLoginHistories.AddAsync(new UserLoginHistory()
                {
                    ApplicationUserId = user.Id,
                    ipAddress = ipAddress,
                    LoginTime = DateTime.UtcNow,

                });
                await _appIdentityDbContext.SaveChangesAsync();
            }
        }

    }
}
