using Youth_Innovation_System.Core.Entities.Identity;

namespace Youth_Innovation_System.Core.Specifications.UserRatingSpecifications
{
    public class GetUserRatingSpecifications : BaseSpecification<UserRating>
    {
        public GetUserRatingSpecifications(string userId)
            : base(ur => ur.ratedUserId == userId)
        {

        }
    }
}
