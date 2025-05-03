using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices.IdentityServices;
using Youth_Innovation_System.Core.Specifications.UserRatingSpecifications;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.UserRating;

namespace Youth_Innovation_System.Service.IdentityServices
{
    public class UserRatingServices : IUserRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRatingServices(IUnitOfWork unitOfWork,
                                  UserManager<ApplicationUser> userManager)

        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<ApiResponse> AddOrUpdateUserRatingAsync(string RatedUserId, string raterId, int rating, string Review)
        {
            var ratedUser = await _userManager.FindByIdAsync(RatedUserId);

            if (ratedUser == null)
                return new ApiResponse(StatusCodes.Status404NotFound, "Rated user is not found");

            if (RatedUserId == raterId)
                return new ApiResponse(StatusCodes.Status400BadRequest, "User cannot rate itself!");

            var userRatingRepo = _unitOfWork.Repository<UserRating>();

            UpdateExistingRateSpecifications spec = new UpdateExistingRateSpecifications(RatedUserId, raterId);

            var existingRating = await userRatingRepo.GetWithSpecAsync(spec);

            if (existingRating != null)
            {
                existingRating.Rating = rating;
                existingRating.Review = Review;
            }
            else
            {
                var newRating = new UserRating()
                {
                    ratedUserId = RatedUserId,
                    raterUserId = raterId,
                    Rating = rating,
                    Review = Review,
                    createdon = DateTime.UtcNow,
                };
                await userRatingRepo.AddAsync(newRating);
            }
            await _unitOfWork.CompleteAsync();
            return new ApiResponse(StatusCodes.Status200OK, "Rating saved successfully");
        }

        public async Task<double> GetUserAverageRatingAsync(string userId)
        {

            var ratings = await GetUserRatingsFromDbAsync(userId);

            if (!ratings.Any()) return 0.0;

            return ratings.Average(r => r.Rating);
        }

        public async Task<IEnumerable<UserRatingDto>> GetUserRatingsAsync(string userId)
        {
            var ratings = await GetUserRatingsFromDbAsync(userId);

            var ratingUserIds = ratings.Select(r => r.raterUserId).ToList();

            var raterUsers = await _userManager.Users.Where(u => ratingUserIds.Contains(u.Id)).ToListAsync();

            var userRatings = ratings.Select(r =>
                 {
                     var raterUser = raterUsers.FirstOrDefault(u => u.Id == r.raterUserId);
                     return new UserRatingDto
                     {
                         CreatedAt = r.createdon,
                         RatingValue = r.Rating,
                         RaterName = raterUser?.firstName,
                         RaterImage = raterUser?.pictureUrl,
                     };
                 });

            return userRatings;

        }
        private async Task<IReadOnlyList<UserRating>> GetUserRatingsFromDbAsync(string userId)
        {
            GetUserRatingSpecifications spec = new GetUserRatingSpecifications(userId);
            return await _unitOfWork.Repository<UserRating>().GetAllWithSpecAsync(spec);
        }

    }
}
