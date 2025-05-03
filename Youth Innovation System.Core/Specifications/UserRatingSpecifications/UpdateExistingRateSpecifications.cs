using Youth_Innovation_System.Core.Entities.Identity;

namespace Youth_Innovation_System.Core.Specifications.UserRatingSpecifications
{
    public class UpdateExistingRateSpecifications : BaseSpecification<UserRating>
    {

        public UpdateExistingRateSpecifications(string RatedUserId, string RaterId)
            : base(ur => ur.ratedUserId == RatedUserId && ur.raterUserId == RaterId)
        {

        }

    }
}
