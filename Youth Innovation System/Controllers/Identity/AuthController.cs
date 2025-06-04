﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youth_Innovation_System.Core.IServices.IdentityServices;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;
        private readonly IUserVerificationService _userVerificationService;

        public AuthController(IAuthService authService, IPasswordService passwordService, IUserVerificationService userVerificationService)
        {
            _authService = authService;
            _passwordService = passwordService;
            _userVerificationService = userVerificationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (result.Succeeded)
            {
                try
                {
                    //Send Verification Email
                    await _userVerificationService.RequestVerificationEmailAsync(registerDto.Email);
                    return Ok(new ApiResponse(StatusCodes.Status200OK, "Registration successful! Please check your email for a verification link to activate your account."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "There is an error while sending verification email."));
                }

            }
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "There is an error, Please register again later!"));
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

        [HttpPost("Rotate-Refresh-Token")]
        public async Task<IActionResult> RotateRefreshToken()
        {
            var token = Request.Cookies["refreshToken"];
            var result = await _authService.RotateRefreshTokenAsync(token);
            if (result.IsAuthenticated)
                return Ok(result);
            return BadRequest(result);
            //wNd12BUKDG6zB1IEqvskqqFpWAY26xJuvs2Gj4YrUCw % 3D
        }
        [HttpPost("Revoke-Refresh-Token")]
        public async Task<IActionResult> RevokeRefreshToken(RevokeRefreshTokenDto revokeRefreshTokenDto)
        {
            //accepting token from body or cookies
            var token = revokeRefreshTokenDto.token ?? Request.Cookies["refreshToken"];
            if (token == null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Token is Required!"));
            var result = await _authService.RevokeRefreshTokenAsync(token);
            if (!result)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Invalid token!"));

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Token Revoked successfully!"));
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
        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            // Confirm the user's email
            var result = await _authService.ConfirmEmailAsync(userId, token);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("Request-Reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var Response = await _userVerificationService.RequestPasswordResetAsync(request.Email);
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _passwordService.ResetPasswordAsync(model.email, model.token, model.newPassword);
            if (!result.Succeeded)
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, Description = result.Errors });

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Password has been reset successfully."));
        }


    }
}
