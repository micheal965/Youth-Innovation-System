using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class AddOrRemoveReactionSpecifications : BaseSpecification<React>
    {
        public AddOrRemoveReactionSpecifications(int postId, string userId)
            : base(r => r.PostId == postId && r.UserId == userId)
        {

        }
    }
}
