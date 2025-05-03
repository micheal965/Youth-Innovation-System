using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.UserRating;

namespace Youth_Innovation_System.Core.IServices.IdentityServices
{
    public interface IUserRatingService
    {

        Task<ApiResponse> AddOrUpdateUserRatingAsync(string RatedUserId, string raterId, int rating, string Review);
        Task<double> GetUserAverageRatingAsync(string userId);
        Task<IEnumerable<UserRatingDto>> GetUserRatingsAsync(string userId);
    }
}
