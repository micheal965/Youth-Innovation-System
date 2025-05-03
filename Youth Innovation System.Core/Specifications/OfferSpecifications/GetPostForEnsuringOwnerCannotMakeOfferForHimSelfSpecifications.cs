using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.OfferSpecifications
{
    public class GetPostForEnsuringOwnerCannotMakeOfferForHimSelfSpecifications : BaseSpecification<Post>
    {
        public GetPostForEnsuringOwnerCannotMakeOfferForHimSelfSpecifications(int postId)
            : base(p => p.Id == postId)
        {
        }
    }
}
