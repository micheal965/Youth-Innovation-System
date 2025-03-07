using Microsoft.AspNetCore.Identity;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Core.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<string> CreateJwtWebTokenAsync(ApplicationUser user);
        Task<bool> IsTokenBlacklistedAsync(string token);
        Task BlacklistTokenAsync(string token);
        //confirm email in db
        Task<ApiResponse> ConfirmEmailAsync(string userId, string token);
        Task<RotateRefreshTokenResponseDto> RotateRefreshTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
