using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.DTOs.Identity;
<<<<<<< HEAD
=======
using Youth_Innovation_System.Service;
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
		private readonly IEmailService _emailService;

		public AuthController(IAuthService authService,IEmailService emailService)
        {
            _authService = authService;
			_emailService = emailService;
		}

        // Register a new user
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (result.Succeeded)
            {
                // Automatically login after successful registration
                var loginDto = new LoginDto()
                {
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                };

                var loginResponse = await _authService.LoginAsync(loginDto);
                return Ok(loginResponse); // Returning login response (including JWT token)

            }
            return BadRequest();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResponse = await _authService.LoginAsync(loginDto);
                return Ok(loginResponse); // Returning login response (including JWT token)
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
        }

		[HttpPost("send")]
		public async Task<IActionResult> SendEmail(string to, string subject , string body)
		{
			await _emailService.SendEmailAsync(to,subject,  body);
			return Ok("Email Sent Successfully!");
		}
		[Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromHeader] string authorization)
        {
            if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer "))
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Invalid Authorization header"));
            }

            var token = authorization.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Token is missing or invalid"));
            }
            await _authService.BlacklistTokenAsync(token);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Logged out successfully"));
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _authService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "Password Changed successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiExceptionResponse(StatusCodes.Status400BadRequest, "Something went wrong while changing password", ex.Message));
            }
        }

   //     [HttpPost("Forgot-Password")]
   //     public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
   //     {
			//var Response = await _authService.SendOtpAsync(request);
   //         return StatusCode(Response.StatusCode, Response);

   //     }

        //[HttpPost("Verify-OTP")]
        //public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto request)
        //{
        //    var Response = await _authService.VerifyOtpAsync(request);
        //    return StatusCode(Response.StatusCode, Response);

        //}

<<<<<<< HEAD
        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var Response = await _authService.ResetPasswordAsync(request);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpGet("login-history/{userId}")]
        public async Task<IActionResult> GetLoginHistory(string userId)
        {
            var historyList = await _authService.GetLoginHistory(userId);
            if (historyList == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "There is no login history"));

            return Ok(historyList);
        }
=======
   //     [HttpPost("Reset-password")]
   //     public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
   //     {
			//var Response = await _authService.ResetPasswordAsync(request);
   //         return StatusCode(Response.StatusCode, Response);
   //     }
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f
    }
}
