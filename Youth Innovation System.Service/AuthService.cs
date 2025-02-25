using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.DTOs.Identity;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Youth_Innovation_System.Service
{
    public class AuthService : IAuthService
    {
        private static readonly HashSet<string> BlacklistedTokens = new();

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(IConfiguration configuration,
                           IUserService userService,
                           UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
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
            //assume no lockout
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.IsPersistent, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid login attempt!");
            }
            if (!user.EmailConfirmed)
            {
                throw new UnauthorizedAccessException("Email is not confirmed. Please verify your email first.");
            }
            //track ipAddress in userloginhistory table
            await _userService.SaveLoginAttempt(loginDto.Email);
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
        //confirm email in db
        public async Task<ApiResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Invalid Email");
            // Confirm the user's email
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Invalid or expired token.");

            return new ApiResponse(StatusCodes.Status200OK, "Email Verified Successfully");
        }
    }
}
