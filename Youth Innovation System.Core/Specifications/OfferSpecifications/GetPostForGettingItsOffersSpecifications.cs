using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.OfferSpecifications
{
    public class GetPostForGettingItsOffersSpecifications : BaseSpecification<Post>
    {
        public GetPostForGettingItsOffersSpecifications(int postId, string userId)
        : base(p => p.Id == postId && p.UserId == userId)

        {
            Includes.Add(p => p.Offers);
        }
    }
}
