using Microsoft.AspNetCore.Identity;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Core.IServices
{
    public interface IAuthService
    {
        public Task<string> CreateWebTokenAsync(ApplicationUser user);
        public Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        public Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    }
}
