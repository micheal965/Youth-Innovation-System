using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public UserController(IUserService userService, IPasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        [HttpGet("login-history/{userId}")]
        public async Task<IActionResult> GetLoginHistory(string userId)
        {
            var historyList = await _userService.GetLoginHistory(userId);
            if (historyList == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "There is no login history"));
            return Ok(historyList);
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _passwordService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "Password Changed successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiExceptionResponse(StatusCodes.Status400BadRequest, "Something went wrong while changing password", ex.Message));
            }
        }
    }
}
