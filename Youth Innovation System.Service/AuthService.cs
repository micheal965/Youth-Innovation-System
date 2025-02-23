using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
<<<<<<< HEAD
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Repository.Identity;
=======
using System.Threading.Channels;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.DTOs.Identity;
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Service
{
    public class AuthService : IAuthService
    {
        private static readonly HashSet<string> BlacklistedTokens = new();

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppIdentityDbContext _appIdentityDbContext;
        private readonly IConfiguration _configuration;
        public AuthService()
        {

        }
        public AuthService(AppIdentityDbContext appIdentityDbContext, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _appIdentityDbContext = appIdentityDbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            //Ensuring user exist by email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid login attempt!");
            }
            //checking password
            //assume no persistent and no lockout
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.IsPersistent, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid login attempt!");
            }
            //track ipAddress in userloginhistory table
            await SaveLoginAttempt(loginDto.Email);
            //returning Response
            var roles = await _userManager.GetRolesAsync(user);
            return new LoginResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = await CreateWebTokenAsync(user),
                Roles = roles.ToList(),
            };
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                UserName = registerDto.Email.Split("@")[0],
                Email = registerDto.Email,
                firstName = registerDto.FirstName,
                lastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                pictureUrl = registerDto.ProfilePictureUrl,
            };
            //ensuring email doesn't exist before
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                throw new Exception($"Email {registerDto.Email} is already taken!");

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            // Generate email confirmation token
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);

            //// Send the email
            //var emailSubject = "Email Confirmation";
            //var emailMessage = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";
            //await _emailSender.SendEmailAsync(user.Email, emailSubject, emailMessage);
            return result;
        }
        public async Task<string> CreateWebTokenAsync(ApplicationUser user)
        {
            //Authentication Claims
            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            //RoleClaims 
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                    authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: authclaims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: cred
                );

            //write token and return
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            await Task.Delay(100); // Simulate a delay
            return BlacklistedTokens.Contains(token);
        }

        // Simulate async I/O operation to add token to blacklist
        public async Task BlacklistTokenAsync(string token)
        {
            await Task.Delay(100); // Simulate a delay
            BlacklistedTokens.Add(token);
        }

        public async Task<ApiResponse> ChangePasswordAsync(string userId, ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse(StatusCodes.Status404NotFound, "user not found");
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!checkPassword)
            {
                return new ApiResponse(StatusCodes.Status400BadRequest,"Old Password is incorrect");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return new ApiExceptionResponse( StatusCodes.Status400BadRequest, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
            return new ApiResponse(StatusCodes.Status200OK , "Password Changed successfully");
        }

        //Forget Password
        //public async Task<ApiResponse> SendOtpAsync(ForgotPasswordRequestDto request)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    if (user == null) return new ApiResponse(StatusCodes.Status404NotFound, "User not found");

        //    var otp = new Random().Next(100000, 999999).ToString();
        //    user.PasswordResetOTP = otp;
        //    user.OTPExpiry = DateTime.UtcNow.AddMinutes(5);
        //    await _userManager.UpdateAsync(user);

        //    await _emailService.SendOtpEmailAsync(user.Email, otp);
        //    return new ApiResponse(StatusCodes.Status200OK, "OTP sent successfully");
        //}

        //public async Task<ApiResponse> VerifyOtpAsync(VerifyOtpRequestDto request)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    if (user == null || user.PasswordResetOTP != request.OTP || user.OTPExpiry < DateTime.UtcNow)
        //        return new ApiResponse(StatusCodes.Status400BadRequest, "Invalid or expired OTP");

        //    return new ApiResponse(StatusCodes.Status200OK, "OTP verified successfully");

        //}

        //public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordRequestDto request)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    if (user == null || user.PasswordResetOTP != request.OTP || user.OTPExpiry < DateTime.UtcNow)
        //        return new ApiResponse(StatusCodes.Status400BadRequest, "Invalid or expired OTP");


        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        //    if (!result.Succeeded) return new ApiResponse(StatusCodes.Status400BadRequest, "Password reset failed");

        //    user.PasswordResetOTP = null;
        //    user.OTPExpiry = null;
        //    await _userManager.UpdateAsync(user);
        //    return new ApiResponse(StatusCodes.Status400BadRequest, "Password reset failed");

<<<<<<< HEAD
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
                var ipAddress = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
=======
        //}
>>>>>>> 92cbe13a0af084ac8d397beb9a1040c95b16841f

                if (string.IsNullOrEmpty(ipAddress))
                    ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();//Fetch IP here

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
