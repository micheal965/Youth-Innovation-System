using Microsoft.AspNetCore.Mvc;
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
    }
}
