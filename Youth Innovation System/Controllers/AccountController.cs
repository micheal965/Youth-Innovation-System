using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.API.Errors;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
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
                    Password = registerDto.Password
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
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromHeader] string authorization)
        {
            if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer "))
            {
                return BadRequest(new { message = "Invalid Authorization header" });
            }

            var token = authorization.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing or invalid" });
            }
            await _authService.BlacklistTokenAsync(token);

            return Ok(new { message = "Logged out successfully" });
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userId = null;
            if(userId is null)
                return BadRequest();
            var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);
            if (result.Succeeded)
            {
                return Ok("Password Changed successfully");
            }
            return BadRequest("something went wrong");
        }
    }
}
