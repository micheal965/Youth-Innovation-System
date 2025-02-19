using Microsoft.AspNetCore.Identity;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Core.IServices
{
    public interface IAuthService
    {
        public Task<string> CreateWebTokenAsync(ApplicationUser user);
        public Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        public Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        public Task<bool> IsTokenBlacklistedAsync(string token);
        public Task BlacklistTokenAsync(string token);
        public Task<ApiResponse> ChangePasswordAsync(string userId, ChangePasswordDto model);
        //public Task<ApiResponse> SendOtpAsync(ForgotPasswordRequestDto request);
        //public Task<ApiResponse> VerifyOtpAsync(VerifyOtpRequestDto request);
        //public Task<ApiResponse> ResetPasswordAsync(ResetPasswordRequestDto request);

    }
}
