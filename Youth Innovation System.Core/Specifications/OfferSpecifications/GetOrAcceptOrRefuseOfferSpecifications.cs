using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.OfferSpecifications
{
    public class GetOrAcceptOrRefuseOfferSpecifications : BaseSpecification<Offer>
    {
        public GetOrAcceptOrRefuseOfferSpecifications(string postOwnerId, int offerId)
            : base(o => o.Post.CreatedBy == postOwnerId && o.Id == offerId)
        {
            Includes.Add(o => o.Post);
        }
    }
}
