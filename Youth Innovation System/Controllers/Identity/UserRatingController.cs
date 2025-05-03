using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.IdentityServices;
using Youth_Innovation_System.Shared.DTOs.UserRating;

namespace Youth_Innovation_System.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingController : ControllerBase
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }
        [HttpPost("Add-Or-Update-Rating")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdateRating(AddOrUpdateRatingDto addOrUpdateRatingDto)
        {
            var RaterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userRatingService.AddOrUpdateUserRatingAsync(
                                                       addOrUpdateRatingDto.ratedUserId,
                                                       RaterId,
                                                       addOrUpdateRatingDto.Rating,
                                                       addOrUpdateRatingDto.Review);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Get-Average-Rating/{userId}")]
        public async Task<IActionResult> GetUserAverageRating(string userId)
        {
            var averageRatings = await _userRatingService.GetUserAverageRatingAsync(userId);
            return Ok(new { averageRating = averageRatings });
        }
        [HttpGet("Get-Ratings/{userId}")]
        public async Task<IActionResult> GetUserRatings(string userId)
        {
            var Ratings = await _userRatingService.GetUserRatingsAsync(userId);
            return Ok(new { Ratings });
        }
    }
}
