using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class GetReactsSpecifications : BaseSpecification<React>
    {
        public GetReactsSpecifications(int postId)
            : base(r => r.PostId == postId)
        {

        }
    }
}
